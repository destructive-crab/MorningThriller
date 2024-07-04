using System;
using Cysharp.Threading.Tasks;
using MothDIed;
using UnityEngine;
using UnityEngine.UI;

namespace rainy_morning.Tests
{
    public class SaveAndExitButton : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(SaveAndExit);
        }

        private void SaveAndExit()
        {
            Game.RunData.Save();
            Application.Quit();
        }
    }
}