using MorningThriller.PlayerLogic;
using UnityEngine;

namespace MorningThriller.GameLoop
{
    public sealed class RunData
    {
        public PlayerData PlayerData { get; private set; }

        private readonly string storagePath;
        private FileDataHandler dataHandler;

        public RunData()
        {
            storagePath = Application.persistentDataPath;
            dataHandler = new FileDataHandler(storagePath, "CurrentRun");
            
            Load();
        }

        public void Save()
        {
            dataHandler.Save(PlayerData);
        }
        
        public void Load() 
        {
            PlayerData = dataHandler.Load<PlayerData>();

            if (PlayerData == null)
            {
                PlayerData = new PlayerData();
            }
        }
    }
}