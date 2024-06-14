using System;
using UnityEngine;

namespace destructive_code.LevelGeneration
{
    [RequireComponent(typeof(PassageHandler))]
    public class RoomBase : DepressedBehaviour
    {
        [field: SerializeField] public Vector2 RoomSize { get; set; }    
        public PassageHandler PassageHandler { get; private set; }

        private void Awake()
        {
            InitPassageHandler();
        }

        private void InitPassageHandler() => PassageHandler = GetComponent<PassageHandler>();

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position, RoomSize);
        }

        public Vector2 GetOffCenter(Direction direction)
        {
            if(PassageHandler == null) 
                InitPassageHandler();
            
            return PassageHandler.GetPassage(direction).Factory.Offset;
        }
    }
}