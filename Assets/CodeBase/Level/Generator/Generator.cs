using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using MothDIed;
using UnityEngine;

namespace MorningThriller.LevelGeneration
{
    public sealed class Generator
    {
        public int CurrentRoomsCount => _spawnedRooms.Count;
        
        private readonly LevelConfig _levelConfig;

        private readonly List<RoomBase> _spawnedRooms = new();

        private Transform container;

        public void Generate(Transform container, Level level)
        {
            this.container = container;
            
            Queue<RoomBase> mustSpawnQueue = new Queue<RoomBase>();
            
            Queue<RoomBase> roomQueue = new Queue<RoomBase>();
            RoomBase startRoomPrefab = level.GetRandomRoomFor(RoomType.StartRoom, Direction.Bottom);

            RoomBase startRoom = Game.CurrentScene.Fabric.Instantiate(startRoomPrefab, Vector3.zero, Quaternion.identity);

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
                        AddSpawnedRoom(room);
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

            foreach (var room in _spawnedRooms)
            {
                room.OnFinish();
            }
        }

        public void AddSpawnedRoom(RoomBase room)
        {
            _spawnedRooms.Add(room);
            room.transform.parent = container;
        }
    }
}
