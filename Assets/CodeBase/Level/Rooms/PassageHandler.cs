using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace destructive_code.LevelGeneration
{
    [RequireComponent(typeof(RoomBase))]
    public sealed class PassageHandler : DepressedBehaviour
    {
        private readonly Dictionary<Direction, Passage> passages = new();
        private RoomBase room;

        public void InitRoomRef()
        {
            room = GetComponent<RoomBase>();
        }

        public void ProcessPassagesAndFactories()
        {
            var passagesList = GetComponentsInChildren<Passage>();
            
            foreach (var passage in passagesList)
            {
                passage.UpdateReferences();
                
                passage.Factory.Init(room);
                passage.Factory.Init(passage.Direction);
                
                passages.TryAdd(passage.Direction, passage);
            }
        }

        public bool Contains(Direction direction) => passages.ContainsKey(direction);

        public Passage FitIn(Transform who)
        {
            foreach (var passage in passages)
            {
                if (passage.Value.Tilemap.cellBounds.Contains(passage.Value.Tilemap.WorldToCell(new Vector3Int((int) who.position.x, (int) who.position.y, 0))))
                {
                    return passage.Value;
                }
            }

            return null;
        }

        public void OpenAllDoors()
        {
            foreach (var passage in passages)
                passage.Value.OpenDoor();
        }

        public void CloseAllDoors()
        {
            foreach (var passage in passages)
                passage.Value.CloseDoor();
        }
        
        public Passage GetPassage(Direction direction)
        {
            return passages.TryGetValue(direction, out var passage) ? passage : null;
        }

        public bool TryGetFree(out Passage freePassage)
        {
            foreach (var passage in passages)
            {
                if (passage.Value.ConnectedRoom == null)
                {
                    freePassage = passage.Value;
                    return true;
                }
            }

            freePassage = null;
            return false;
        }

        public void BuildPassages(List<Direction> directions)
        {
            int firstToDelete = Random.Range(0, 4);            
            int second = Random.Range(0, 4);

            if (!directions.Contains((Direction)firstToDelete) && passages.ContainsKey((Direction)firstToDelete))
            {
                passages[(Direction)firstToDelete].DisablePassage();
                passages.Remove((Direction)firstToDelete);
            }

            if (firstToDelete != second && !directions.Contains((Direction)second) && passages.ContainsKey((Direction)second))
            {
                passages[(Direction)second].DisablePassage();
                passages.Remove((Direction) second);
            }
        }

        public void CloseAllFree()
        {
            List<Direction> toRemove = new();

            foreach (var passage in passages)
            {
                if (passage.Value.ConnectedRoom == null)
                {
                    passage.Value.DisablePassage();                    
                    toRemove.Add(passage.Key);
                }
            }

            foreach (var direction in toRemove)
                passages.Remove(direction);
        }
    }
}