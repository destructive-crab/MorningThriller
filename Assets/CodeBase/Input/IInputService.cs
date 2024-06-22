using System;
using UnityEngine;

namespace destructive_code.InputManagement
{
    public interface IInputService
    {
        //UI
        void SwitchToUIInputs();
        
        //player

        void SwitchToPlayerInputs();

        event Action OnItemUsed;
        event Action OnRoll;
        
        Vector2 GetMovement();
        float GetMouseScroll();
        
    }
}