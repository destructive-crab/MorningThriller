using Cinemachine;
using UnityEngine;

namespace destructive_code.LevelGeneration.CameraManagement
{
    public sealed class RoomCameraManager
    {
        public CinemachineVirtualCamera VirtualCamera { get; private set; }

        public RoomCameraManager(Transform room)
        {
            VirtualCamera = room.GetComponentInChildren<CinemachineVirtualCamera>();
        }
    }
}