using MorningThriller.InputManagement;
using MorningThriller.LevelGeneration.CameraManagement;
using MorningThriller.PlayerLogic;
using MorningThriller.Tests;
using MothDIed.Scenes;
using UnityEngine;

namespace MorningThriller.LevelGeneration
{
    public class LevelScene : Scene
    {
        public Level Level { get; private set; }
        public PlayerRoot Player { get; private set; }
        public CameraSwitcher CameraSwitcher { get; private set; }

        public readonly IInputService InputService = new DesktopInputService();
        
        protected readonly LevelConfig config;

        public LevelScene(LevelConfig config)
        {
            this.config = config;
        }

        public override string GetSceneName()
        {
            return "Level";
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

            Player = GameObject.FindObjectOfType<PlayerRoot>();
            CameraSwitcher = new CameraSwitcher();
            
            InputService.SwitchToPlayerInputs();
        }
    }
}