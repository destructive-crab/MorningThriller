using UnityEngine;

namespace MorningThriller.LevelGeneration
{
    [CreateAssetMenu]
    public sealed class LevelConfig : ScriptableObject
    {
        [field: SerializeField] public int TotalRoomsCount { get; private set; }
        [field: SerializeField] public int TraderRoomsCount { get; private set; }
        [field: SerializeField] public int NPCRoomsCount  { get; private set; }

        public RoomBase[] CombatRooms;
        public RoomBase[] TreasureRooms;
        public RoomBase[] NPCRooms;
        public RoomBase[] TraderRooms;
        public RoomBase[] ExitRooms;
    }
}