using UnityEngine;

namespace destructive_code.Level
{
    // [RequireComponent(typeof(PassageHandler))]
    public class RoomBase : DepressedBehaviour
    {
        [field: SerializeField] public Vector2 RoomSize { get; private set; }    
        // public PassageHandler PassageHandler { get; private set; }
    }
}