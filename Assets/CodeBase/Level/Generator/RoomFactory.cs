using UnityEngine;

namespace destructive_code.LevelGeneration
{
    public abstract class RoomFactory : MonoBehaviour
    {
        [field: SerializeField, HideInInspector]public Vector2 Offset { get; private set; }

        public void SetOffset(Vector2 offset)
        {
            Offset = offset;
        }
        
        public abstract void Init(RoomBase owner, Direction direction);
        public abstract RoomBase CreateByType(RoomType roomType, bool remove = true);
        public abstract RoomBase CreateByPrefab(RoomBase roomToSpawn);
    }
}