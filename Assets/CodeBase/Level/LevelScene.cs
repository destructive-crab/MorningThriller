using destructive_code.InputManagement;
using destructive_code.LevelGeneration.CameraManagement;
using destructive_code.PlayerCodeBase;
using destructive_code.Scenes;
using destructive_code.Tests;
using UnityEngine;

namespace destructive_code.LevelGeneration
{
    public class LevelScene : Scene
    {
        public Level Level { get; private set; }
        public PlayerDummy PlayerDummy { get; private set; }
        public CameraSwitcher CameraSwitcher { get; private set; }

        public readonly IInputService InputService = new DesktopInputService();
        
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

            PlayerDummy = GameObject.FindObjectOfType<PlayerDummy>();
            CameraSwitcher = new CameraSwitcher();
            
            InputService.SwitchToPlayerInputs();
        }
    }
}