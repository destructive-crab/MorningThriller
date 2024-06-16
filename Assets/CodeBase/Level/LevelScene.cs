using destructive_code.LevelGeneration.CameraManagement;
using destructive_code.LevelGeneration.PlayerCode;
using destructive_code.Scenes;
using destructive_code.Tests;
using UnityEngine;

namespace destructive_code.LevelGeneration
{
    public class LevelScene : Scene
    {
        public Level Level { get; private set; }
        public Player Player { get; private set; }
        public CameraSwitcher CameraSwitcher { get; private set; }

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

            Transform container = new GameObject("Rooms").transform;
            container.gameObject.AddComponent<RoomsContainer>();
            
            Level.Generator.Generate(container, Level);

            Player = GameObject.FindObjectOfType<Player>();
            CameraSwitcher = new CameraSwitcher();
        }
    }
}