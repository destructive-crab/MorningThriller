using System;
using destructive_code.LevelGeneration;
using UnityEngine;

namespace destructive_code.Tests
{
    public class TestLevel : LevelGeneration.Level
    {
        public TestLevel(LevelConfig levelConfig) : base(levelConfig)
        {
            
        }

        public override void CreateMap()
        {
            map = new RoomType[levelConfig.TotalRoomsCount + 2];

            map[0] = RoomType.StartRoom;
            map[map.Length - 1] = RoomType.Exit;

            for (int i = 0; i < levelConfig.TraderRoomsCount; i++)
            {
                int roomID = UnityEngine.Random.Range(1, map.Length - 2);

                if (map[roomID] == RoomType.Combat)
                    map[roomID] = RoomType.Trader;
                else
                    i--;
            }
            for (int i = 0; i < levelConfig.NPCRoomsCount; i++)
            {
                int roomID = UnityEngine.Random.Range(1, map.Length - 2);

                if (map[roomID] == RoomType.Combat) map[roomID] = RoomType.NPC;
                else i--;
            }
        }

        public override RoomBase GetRandomRoomFor(RoomType roomType, Direction direction)
        {
            return roomType switch
            {
                RoomType.StartRoom => GetRandomFor(levelConfig.CombatRooms),
                RoomType.Combat => GetRandomFor(levelConfig.CombatRooms),
                RoomType.Treasure => GetRandomFor(levelConfig.CombatRooms),
                RoomType.NPC => GetRandomFor(levelConfig.CombatRooms),
                RoomType.Trader => GetRandomFor(levelConfig.CombatRooms),
                RoomType.Exit => GetRandomFor(levelConfig.CombatRooms),
                _ => throw new ArgumentOutOfRangeException(nameof(roomType), roomType, null)
            };
        }

        private RoomBase GetRandomFor(RoomBase[] rooms)
        {
            return rooms[UnityEngine.Random.Range(0, rooms.Length)];
        }
    }
}