using System;
using destructive_code.PlayerCodeBase.CommonStates;
using UnityEngine;

namespace destructive_code.PlayerCodeBase.Standard
{
    public class StandardPlayerIdle : PlayerIdle
    {
        public override Type[] CanBeEnteredFrom => Array.Empty<Type>();
        public override bool AllowRepeats { get; } = false;

        public override void Enter(PlayerRoot playerRoot)
        {
            playerRoot.CachedComponents.Get<Animator>().Play("Idle");
            playerRoot.CachedComponents.Get<Rigidbody2D>().velocity = Vector2.zero;
            
            Debug.Log("KKDKK");
        }
    }
}