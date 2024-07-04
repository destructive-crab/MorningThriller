using System;
using MorningThriller.PlayerLogic.CommonStates;
using UnityEngine;

namespace MorningThriller.PlayerLogic.Standard
{
    public class StandardPlayerIdle : PlayerIdle
    {
        public override Type[] CanBeEnteredOnlyFrom => Array.Empty<Type>();
        public override Type[] CannotBeEnteredFrom => Array.Empty<Type>();
        public override bool AllowRepeats { get; } = false;

        public override void Enter(PlayerRoot playerRoot)
        {
            playerRoot.CachedComponents.Get<Animator>().Play("Idle");
            playerRoot.CachedComponents.Get<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}