using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace destructive_code.LevelGeneration.CameraManagement
{
    public sealed class CameraSwitcher
    {
        public CinemachineVirtualCamera CurrentCamera { get; private set; }
        public Transition Transition { get; private set; }

        private readonly List<CinemachineVirtualCamera> cameras;

        public CameraSwitcher()
        {
            cameras = new List<CinemachineVirtualCamera>
                (GameObject.FindObjectOfType<RoomsContainer>().GetComponentsInChildren<CinemachineVirtualCamera>());
            Transition = Transition.Instance;

            foreach (var camera in cameras)
            {
                camera.gameObject.SetActive(false);
            }
            
            cameras[0].gameObject.SetActive(true);
        }

        public void SwitchTo(CinemachineVirtualCamera camera)
        {
            if(CurrentCamera != null)
            {
                CurrentCamera.gameObject.SetActive(false);
            }

            camera.gameObject.SetActive(true);
            CurrentCamera = camera;
        }

        public async void SwitchWTTo(CinemachineVirtualCamera camera)
        {
            await Transition.Enable();
            SwitchTo(camera);
            Transition.Disable();
        }
    }
}