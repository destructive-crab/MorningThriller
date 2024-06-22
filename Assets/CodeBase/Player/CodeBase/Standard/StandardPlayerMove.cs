using System;
using destructive_code.PlayerCodeBase.CommonStates;
using destructive_code.Scenes;
using UnityEngine;

namespace destructive_code.PlayerCodeBase.Standard
{
    public class StandardPlayerMove : PlayerMove
    {
        public override Type[] CanBeEnteredFrom => Array.Empty<Type>();
        public override bool AllowRepeats => false;

        public override void FixedUpdate(PlayerRoot playerRoot)
        {
            playerRoot.CachedComponents.Get<Animator>().Play("Run");
            
            playerRoot.CachedComponents.Get<Rigidbody2D>().velocity =
                SceneSwitcher.LevelScene.InputService.GetMovement();
            
            Debug.Log(SceneSwitcher.LevelScene.InputService.GetMovement());
        }
    }
}