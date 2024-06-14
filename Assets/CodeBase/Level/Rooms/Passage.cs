using System;
using Unity.Collections;
using UnityEngine;

namespace destructive_code.LevelGeneration
{
    public sealed class Passage : DepressedBehaviour
    {
        [field: SerializeField] public Direction Direction { get; private set; }
        [field: NaughtyAttributes.ReadOnly, SerializeField] public RoomFactory Factory { get; private set; }
        public RoomBase ConnectedRoom { get; private set; }

        [SerializeField] private GameObject door;

        private void Awake()
        {
            UpdateFactory();
        }

        private void Reset()
        {
            Vector2 pp = transform.parent.position;
            Vector2 tp = transform.position;

            Vector2 distance = tp - pp;

            if (distance.x < 0 && Math.Abs(distance.x) > Math.Abs(distance.y))
            {
                Direction = Direction.Left;
            }
            else if (distance.x > 0 && Math.Abs(distance.x) > Math.Abs(distance.y))
            {
                Direction = Direction.Right;
            }
            else if (distance.y < 0 && Math.Abs(distance.x) < Math.Abs(distance.y))
            {
                Direction = Direction.Bottom;
            }
            else if (distance.y > 0 && Math.Abs(distance.x) < Math.Abs(distance.y))
            {
                Direction = Direction.Top;
            }
        }

        public void UpdateFactory()
        {
            Factory = GetComponentInChildren<RoomFactory>();
        }

        public void Connect(RoomBase room)
            => ConnectedRoom = room;

        public void OpenDoor()
        {
            if(door != null)
                door.SetActive(false);
        }

        public void CloseDoor()
        {
            if(door != null)
                door.SetActive(true);
        }

        public void DisablePassage()
        {
            gameObject.SetActive(false);
        }
        public void EnablePassage()
        {
            gameObject.SetActive(true);
        }
    }
}