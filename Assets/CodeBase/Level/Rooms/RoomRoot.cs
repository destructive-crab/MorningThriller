namespace destructive_code.LevelGeneration
{
    public sealed class RoomRoot
    {
        private readonly RoomBase room;

        public RoomRoot(RoomBase room)
        {
            this.room = room;
        }

        public void Construct()
        {
            var passageHandler = room.GetComponentInChildren<PassageHandler>();

            passageHandler.InitRoomRef();
            passageHandler.ProcessPassagesAndFactories();
            
            passageHandler.OpenAllDoors();
        }
    }
}