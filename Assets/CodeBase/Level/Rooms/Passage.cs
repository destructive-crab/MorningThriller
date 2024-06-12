using System;
using destructive_code.Level;
using UnityEngine;

namespace destructive_code.Level
{
    public sealed class Passage : DepressedBehaviour
    {
        [field: SerializeField] public Directions Direction { get; private set; }
        public RoomBase ConnectedRoom { get; private set; }

        [SerializeField] private GameObject wall;
        [SerializeField] private GameObject door;

        private void Reset()
        {
            Vector2 pp = transform.parent.position;
            Vector2 tp = transform.position;

            Vector2 distance = tp - pp;

            if (distance.x < 0 && Math.Abs(distance.x) > Math.Abs(distance.y))
            {
                Direction = Directions.Left;
            }
            else if (distance.x > 0 && Math.Abs(distance.x) > Math.Abs(distance.y))
            {
                Direction = Directions.Right;
            }
            else if (distance.y < 0 && Math.Abs(distance.x) < Math.Abs(distance.y))
            {
                Direction = Directions.Bottom;
            }
            else if (distance.y > 0 && Math.Abs(distance.x) < Math.Abs(distance.y))
            {
                Direction = Directions.Top;
            }
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
            wall.SetActive(true);
            gameObject.SetActive(false);
        }
        public void EnablePassage()
        {
            wall.SetActive(false);
            gameObject.SetActive(true);
        }
    }
}