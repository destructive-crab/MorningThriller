namespace MorningThriller.LevelGeneration
{
    public sealed class RoomRoot
    {
        private readonly RoomBase room;

        public RoomRoot(RoomBase room)
        {
            this.room = room;
        }

        public void ConstructInPrefab()
        {
            var passageHandler = room.GetComponentInChildren<PassageHandler>();

            passageHandler.InitRoomRef();
            passageHandler.ProcessPassagesAndFactories();
            
            passageHandler.OpenAllDoors();
        }

        public void ConstructOnFinish()
        {
            var roomCollision = new RoomCollision(room);
            roomCollision.Generate();
        }
        
    }
}