using System;
using destructive_code.Scenes;
using UnityEngine;

namespace destructive_code
{
    public sealed class GameStartPoint : MonoBehaviour
    {
        private void Start()
        {
            SceneSwitcher.SwitchTo(new TestGeneration());
        }
    }
}