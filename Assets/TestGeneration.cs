using destructive_code.Scenes;
using UnityEngine;

namespace destructive_code
{
    public sealed class TestGeneration : Scene
    {
        public override string GetSceneName()
        {
            return "Generation";
        }

        public override Camera GetCamera()
        {
            return Camera.current;
        }

        protected override void OnSceneLoaded()
        {
        
        }
    }
}