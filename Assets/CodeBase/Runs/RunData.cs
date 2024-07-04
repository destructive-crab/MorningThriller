using MorningThriller.PlayerLogic;
using Newtonsoft.Json;

namespace MorningThriller.GameLoop
{
    public sealed class RunData
    {
        public PlayerData PlayerData { get; private set; }

        public void Save()
        {
            string data = JsonConvert.SerializeObject(PlayerData);
            
            
        }
        
        public void Load() 
        {
        
        }
    }
}