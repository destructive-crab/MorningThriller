using System;
using MorningThriller.PlayerLogic.CommonStates;
using MothDIed;
using UnityEngine;

namespace MorningThriller.PlayerLogic.Standard
{
    public class StandardPlayerMove : PlayerMove
    {
        public override Type[] CanBeEnteredOnlyFrom => Array.Empty<Type>();
        public override Type[] CannotBeEnteredFrom => Array.Empty<Type>();
        public override bool AllowRepeats => false;

        public override void FixedUpdate(PlayerRoot playerRoot)
        {
            playerRoot.CachedComponents.Get<Animator>().Play("Run");
            
            playerRoot.CachedComponents.Get<Rigidbody2D>().velocity =
                Game.LevelScene.InputService.GetMovement();
        }
    }
}