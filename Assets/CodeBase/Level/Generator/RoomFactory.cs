using UnityEngine;

namespace MorningThriller.LevelGeneration
{
    public abstract class RoomFactory : MonoBehaviour
    {
        [field: SerializeField, HideInInspector] public Vector2 Offset { get; private set; }

        public void SetOffset(Vector2 offset)
        {
            Offset = offset;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(Offset, 0.2f);
        }

        public abstract RoomBase CreateByType(RoomType roomType, bool remove = true);
        public abstract RoomBase CreateByPrefab(RoomBase roomToSpawn);
        public abstract void Init(RoomBase owner);
        public abstract void Init(Direction direction);
    }
}