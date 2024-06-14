using System;
using System.Collections.Generic;
using destructive_code.Scenes;
using NaughtyAttributes;
using UnityEngine;

namespace destructive_code.LevelGeneration
{
    public class CommonRoomFactory : RoomFactory
    {
        [SerializeField, ReadOnly] private Direction direction;
        private RoomBase owner;
        
        private Level level;
        
        public override void Init(RoomBase owner, Direction direction)
        {
            if (this.owner == null)
            {
                this.owner = owner;
            }
            this.direction = direction;
        }

        private void Awake()
        {
            level = SceneSwitcher.LevelScene.Level;
        }

        public override RoomBase CreateByType(RoomType roomType, bool remove = true)
        {
            var roomToSpawn = level.GetRandomRoomFor(roomType, direction);
            
            var createdRoom = CreateByPrefab(roomToSpawn);
            
            if(createdRoom != null && remove) 
                level.Remove(roomToSpawn, roomType);

            return createdRoom;
        }

        public override RoomBase CreateByPrefab(RoomBase roomToSpawn)
        {
            transform.localPosition = GetPosition(roomToSpawn);

            if (!CheckPlace(roomToSpawn))
                return null;

            var spawnedRoom = SpawnRoom(roomToSpawn);

            ConnectRooms(spawnedRoom);

            spawnedRoom.PassageHandler.BuildPassages(new List<Direction> { DirectionHelper.GetOpposite(direction) });

            return spawnedRoom;
        }

        private RoomBase SpawnRoom(RoomBase roomToSpawn)
        {
            var spawnedRoom = SceneSwitcher.CurrentScene.Fabric
                .Instantiate<RoomBase>(roomToSpawn, transform.position, Quaternion.identity);
            return spawnedRoom;
        }

        private void ConnectRooms(RoomBase spawnedRoom)
        {
            spawnedRoom.PassageHandler.GetPassage(DirectionHelper.GetOpposite(direction)).Connect(owner);
            owner.PassageHandler.GetPassage(direction).Connect(spawnedRoom);
        }

        private bool CheckPlace(RoomBase roomToSpawn)
        {
            var roomPlaceMask = LayerMask.GetMask("RoomPlace");
            
            var overlapResult = Physics2D.OverlapBoxAll(transform.position, roomToSpawn.RoomSize, 0f, roomPlaceMask);

            foreach (var hit2D in overlapResult) 
            {
                if (hit2D.transform.gameObject.TryGetComponent(out RoomBase room) && room != owner)
                    return false;
            }

            return true;
        }

        private Vector3 GetPosition(RoomBase roomToSpawn)
        {
            return owner.GetOffCenter(direction) - roomToSpawn.GetOffCenter(DirectionHelper.GetOpposite(direction));
        }
    }
}