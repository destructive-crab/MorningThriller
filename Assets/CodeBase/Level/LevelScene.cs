using destructive_code.Scenes;
using destructive_code.Tests;
using UnityEngine;

namespace destructive_code.LevelGeneration
{
    public class LevelScene : Scene
    {
        public Level Level { get; private set; }

        protected LevelConfig config;

        public LevelScene(LevelConfig config)
        {
            this.config = config;
        }

        public override string GetSceneName()
        {
            return "TestScene";
        }

        public override Camera GetCamera()
        {
            return Camera.current;
        }

        protected override void OnSceneLoaded()
        {
            Level = new TestLevel(config);
            
            Level.CreateMap();
            Level.Generator.Generate(new GameObject("Rooms").transform, Level);
        }
    }
}