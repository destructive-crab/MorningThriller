using MorningThriller.LevelGeneration;
using MothDIed;

namespace MorningThriller
{
    public sealed class MorningThrillerStartPoint : GameStartPoint
    {
        public LevelConfig Level;
        
        protected override void StartGame()
        {
            Game.SwitchTo(new LevelScene(Level));
        }
    }
}