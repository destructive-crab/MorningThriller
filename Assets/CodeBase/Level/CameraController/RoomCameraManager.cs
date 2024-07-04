using Cinemachine;
using UnityEngine;

namespace MorningThriller.LevelGeneration.CameraManagement
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