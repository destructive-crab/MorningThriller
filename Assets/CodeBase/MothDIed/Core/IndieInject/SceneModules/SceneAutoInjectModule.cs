﻿using MothDIed.Scenes;
using MothDIed.Scenes.SceneModules;
using UnityEngine;

namespace MothDIed.DI
{
    public class SceneAutoInjectModule : SceneModule
    {
        public override void StartModule(Scene scene)
        {
            var all = GameObject.FindObjectsOfType<MonoBehaviour>(true);

            foreach (var monoBehaviour in all)
            {
                Game.Injector.InjectWithBase(monoBehaviour);
            }
        }
    }
}