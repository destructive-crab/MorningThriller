using UnityEngine;

namespace destructive_code.Level
{
    // [RequireComponent(typeof(PassageHandler))]
    public class RoomBase : DepressedBehaviour
    {
        [field: SerializeField] public Vector2 RoomSize { get; set; }    
        // public PassageHandler PassageHandler { get; private set; }
        
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position, RoomSize);
        }
    }
}