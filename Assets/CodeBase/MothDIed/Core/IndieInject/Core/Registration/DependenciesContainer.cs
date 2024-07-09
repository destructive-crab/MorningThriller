// **************************************************************** //
//
//   Copyright (c) YUJECK. All rights reserved.
//   Contact me: 
//          - GitHub:   https://github.com/YUJECK
//
// **************************************************************** //

using System;
using System.Collections.Generic;
using MothDIed.ServiceLocators;
using UnityEngine;

namespace MothDIed.DI
{
    public sealed class DependenciesContainer : IDisposable, IServiceLocator
    {
        private readonly Dictionary<Type, Dependency> dependencies = new ();

        public void Dispose()
        {
            foreach (var dependencyPair in dependencies)
            {
                dependencyPair.Value.Dispose();
            }
        }

        public bool Contains<TDependency>() where TDependency : class
        {
            return Contains(typeof(TDependency));
        }

        public bool Contains(Type dependencyType)
        {
            return dependencies.ContainsKey(dependencyType);
        }

        public object Get(Type extensionType)
        {
            if (!Contains(extensionType))
                return null;
            
            return GetDependency(extensionType).GetInstance();
        }

        public Dependency GetDependency(Type type)
        {
            if (dependencies.TryGetValue(type, out var dependency))
            {
                return dependency;
            }

            return null;
        }

        public void Add(Type dependencyType, Dependency dependency)
        {
            if (!dependencies.TryAdd(dependencyType, dependency))
            {
#if UNITY_EDITOR
                Debug.LogWarning($"{dependencyType} DEPENDENCY ALREADY REGISTERED");
#endif
            }
        }

        public void Remove<TDependency>()
        {
            if (dependencies.ContainsKey(typeof(TDependency)))
                dependencies.Remove(typeof(TDependency));
        }

        public void SetupSingletons()
        {
            foreach (var dependencyPair in dependencies)
            {
                if (dependencyPair.Value.IsSingleton)
                {
                    Game.Injector.InjectWithBase(dependencyPair.Value.GetInstance());
                }
            }
        }
    }
}