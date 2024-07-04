namespace MorningThriller.LevelGeneration
{
    public abstract class Level
    {
        public Generator Generator { get; private set; }
        public int Length => map.Length;
        
        protected readonly LevelConfig levelConfig;
        protected RoomType[] map;

        protected Level(LevelConfig levelConfig)
        {
            this.levelConfig = levelConfig;
            Generator = new Generator();
        }

        public abstract void CreateMap();

        public RoomType GetNextRoom()
        {
            return map[Generator.CurrentRoomsCount-1];
        }

        public abstract RoomBase GetRandomRoomFor(RoomType roomType, Direction direction);

        public void Remove(RoomBase roomToSpawn, RoomType roomType)
        {
        }
    }
}