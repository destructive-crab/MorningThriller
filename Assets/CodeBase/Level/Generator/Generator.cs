using System.Collections.Generic;
using destructive_code.Scenes;
using UnityEngine;

namespace destructive_code.LevelGeneration
{
    public sealed class Generator
    {
        public int CurrentRoomsCount => _spawnedRooms.Count;
        
        private readonly LevelConfig _levelConfig;

        private readonly List<RoomBase> _spawnedRooms = new();

        public void Generate(Transform container, Level level)
        {
            var mustSpawnQueue = new Queue<RoomBase>();
            
            var roomQueue = new Queue<RoomBase>();
            var startRoomPrefab = level.GetRandomRoomFor(RoomType.StartRoom, Direction.Bottom);

            var startRoom = SceneSwitcher.CurrentScene.Fabric
                .Instantiate<RoomBase>(startRoomPrefab, Vector3.zero, Quaternion.identity);

            roomQueue.Enqueue(startRoom);
            AddSpawnedRoom(startRoom);

            int i = 0;
            while (CurrentRoomsCount < level.Length && i < level.Length * 4 && roomQueue.Count > 0)
            {
                i++;
                
                if (roomQueue.Peek().PassageHandler.TryGetFree(out var passage))
                {
                    RoomBase room;

                    if (mustSpawnQueue.TryPeek(out var roomPrefab))
                    {
                        room = passage.Factory.CreateByPrefab(roomPrefab);
                    }
                    else
                    {
                        room = passage.Factory.CreateByType(level.GetNextRoom());
                    }

                    if (room != null)
                    {
                        roomQueue.Enqueue(room);
                    }
                    else
                    {
                        roomQueue.Dequeue();
                    }
                }
                else
                {
                    roomQueue.Dequeue();
                }

                if (roomQueue.Count == 0) 
                    break;
            }

            foreach (var room in _spawnedRooms)
                room.PassageHandler.CloseAllFree();
        }

        public void AddSpawnedRoom(RoomBase room) 
            => _spawnedRooms.Add(room);
    }
}
