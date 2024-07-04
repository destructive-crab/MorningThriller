using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MorningThriller.InputManagement
{
    public sealed class DesktopInputService : IInputService
    {
        public event Action OnItemUsed;
        public event Action OnRoll;
        
        private InputMap map;

        public DesktopInputService()
        {
            map = new InputMap();
            
            map.Player.ItemUsed.performed += OnItemUsedPerformed;
            map.Player.Roll.performed += OnRollPerformed;
        }

        public void SwitchToUIInputs()
        {
            map.UI.Enable();
            map.Player.Disable();
        }

        public void SwitchToPlayerInputs()
        {
            map.UI.Disable();
            map.Player.Enable();
        }
        
        private void OnItemUsedPerformed(InputAction.CallbackContext context) => OnItemUsed?.Invoke();
        private void OnRollPerformed(InputAction.CallbackContext context) => OnRoll?.Invoke();

        public Vector2 GetMovement() => map.Player.Move.ReadValue<Vector2>();

        public float GetMouseScroll() => map.Player.ItemScroll.ReadValue<Vector2>().y;
    }
}