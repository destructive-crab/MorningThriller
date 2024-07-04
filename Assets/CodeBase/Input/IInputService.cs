using System;
using UnityEngine;

namespace MorningThriller.InputManagement
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