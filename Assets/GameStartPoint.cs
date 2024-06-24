using System;
using destructive_code.LevelGeneration;
using destructive_code.Scenes;
using UnityEngine;

namespace destructive_code
{
    public sealed class GameStartPoint : MonoBehaviour
    {
        public LevelConfig Level;
        
        private void Start()
        {
            Game.SwitchTo(new LevelScene(Level));
        }
    }
}