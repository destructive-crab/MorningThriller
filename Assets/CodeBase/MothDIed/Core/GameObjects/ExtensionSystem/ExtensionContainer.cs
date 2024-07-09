using System;
using System.Collections.Generic;
using MothDIed.ServiceLocators;
using UnityEngine;

namespace MothDIed.ExtensionSystem
{
    public sealed class ExtensionContainer : IServiceLocator
    {
        private DepressedBehaviour owner;

        private readonly Dictionary<Type, List<Extension>> extensions = new();

        private bool containerStarted = false;

        public void StartContainer(DepressedBehaviour owner)
        {
            this.owner = owner;
            containerStarted = true;

            foreach (var extension in this.extensions)
            {
                extension.Value.ForEach((extension) =>
                {
                    Game.Injector.InjectWithBaseAnd(extension, owner.CachedComponents);
                    extension.StartExtension();
                });
            }
        }

        ~ExtensionContainer()
        {
            foreach (var extension in extensions)
            {
                extension.Value.ForEach(extension => extension.Dispose());
            }
        }

        #region Checks

        public TExtension GetExtension<TExtension>()
            where TExtension : Extension
        {
            return Get(typeof(TExtension)) as TExtension;
        }

        public TExtension[] GetExtensions<TExtension>()
            where TExtension : Extension
        {
            return extensions[typeof(TExtension)].ToArray() as TExtension[];
        }

        public bool Contains<TExtension>() where TExtension : Extension
        {
            return Contains(typeof(TExtension));
        }

        public bool Contains(Type serviceType)
        {
            return extensions.ContainsKey(serviceType);
        }

        public object Get(Type extensionType)
        {
            return extensions[extensionType][0];
        }
        
        #endregion

        #region Extensions Managment

        public void AddExtension(Extension extension)
        {
            extension.Initialize(owner);

            if (Attribute.GetCustomAttribute(extension.GetType(), typeof(DisallowMultipleExtensionsAttribute)) != null)
            {
                if (extensions.ContainsKey(extension.GetType()))
                {
#if UNITY_EDITOR
                    Debug.Log("YOU TRIED TO ADD MULTIPLE EXTENSIONS OF TYPE " + extension.GetType());
#endif
                    return;
                }
            }

            extensions.TryAdd(extension.GetType(), new List<Extension>());
            extensions[extension.GetType()].Add(extension);

            if (containerStarted)
            {
                Game.Injector.InjectWithBaseAnd(extension, owner.CachedComponents);
                
                extension.Enable();
                extension.StartExtension();
            }
        }

        public void RemoveExtension<TExtension>()
            where TExtension : Extension
        {
            Type extension = typeof(TExtension);

            if (extensions.ContainsKey(extension))
            {
                extensions[extension][0].Dispose();
                extensions[extension].RemoveAt(0);

                if (extensions[extension].Count == 0)
                    extensions.Remove(extension);
            }
        }

        public void RemoveAllExtensions<TExtension>()
            where TExtension : Extension
        {
            while (extensions.ContainsKey(typeof(Extension)))
            {
                RemoveExtension<TExtension>();
            }
        }
        
        public void RemoveExtensions<TExtension>(int count)
            where TExtension : Extension
        {
            for(int i = 0; i < count; i++)
            {
                RemoveExtension<TExtension>();
            }
        }

        #endregion

        #region Event Methods For Extensions

        public void EnableContainer()
        {
            foreach (var extensionPair in extensions)
            {
                extensionPair.Value.ForEach(extension => extension.Enable());
            }
        }
        public void DisableContainer()
        {
            foreach (var extensionPair in extensions)
            {
                extensionPair.Value.ForEach(extension => extension.Disable());
            }
        }
        public void UpdateContainer()
        {
            foreach (var extensionPair in extensions)
            {
                extensionPair.Value.ForEach(extension => extension.Update());
            }
        }
        public void FixedUpdateContainer()
        {
            foreach (var extensionPair in extensions)
            {
                extensionPair.Value.ForEach(extension => extension.FixedUpdate());
            }
        }

        #endregion
    }
}