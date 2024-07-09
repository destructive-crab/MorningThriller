using System;
using MorningThriller.Player;
using MorningThriller.PlayerLogic.CommonStates;
using MothDIed;
using MothDIed.DI;
using UnityEngine;

namespace MorningThriller.PlayerLogic.Standard
{
    public class StandardPlayerMove : PlayerMove
    {
        public override Type[] CanBeEnteredOnlyFrom => Array.Empty<Type>();
        public override Type[] CannotBeEnteredFrom => Array.Empty<Type>();
        public override bool AllowRepeats => false;

        [Inject] private PlayerAnimator animator;
        [Inject] private Rigidbody2D rigidbody2D;
        
        public override void FixedUpdate(PlayerRoot playerRoot)
        {
            animator.PlayRun(1);
            rigidbody2D.velocity = Game.LevelScene.InputService.GetMovement();
        }
    }
}