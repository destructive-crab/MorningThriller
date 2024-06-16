using destructive_code.LevelGeneration.CameraManagement;
using UnityEngine;

namespace destructive_code.LevelGeneration
{
    [RequireComponent(typeof(PassageHandler))]
    public class RoomBase : DepressedBehaviour
    {
        [field: SerializeField] public Vector2 RoomSize { get; set; }    
        public PassageHandler PassageHandler { get; private set; }
        public bool Initialized { get; private set; } = false;
        
        public RoomCameraManager CameraManager { get; private set; }
        
        private void Awake()
        {
            if(!Initialized)
                Init();
        }

        public void Init()
        {
            InitPassageHandler();

            var root = new RoomRoot(this);
            root.Construct();

            CameraManager = new RoomCameraManager(this.transform);
            
            Initialized = true;
        }

        private void InitPassageHandler() 
            => PassageHandler = GetComponent<PassageHandler>();

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position, RoomSize);
        }

        public Vector3 GetOffCenter(Direction direction)
        {
            if(PassageHandler == null) 
                InitPassageHandler();

            return PassageHandler.GetPassage(direction).Factory.Offset;
        }
    }
}