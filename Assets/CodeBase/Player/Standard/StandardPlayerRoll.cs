using System;
using System.Collections;
using MorningThriller.PlayerLogic.CommonStates;
using UnityEngine;

namespace MorningThriller.PlayerLogic.Standard
{
    public class StandardPlayerRoll : PlayerRoll
    {
        public override bool AllowRepeats { get; } = false;

        public override Type[] CanBeEnteredOnlyFrom => new[] {typeof(PlayerMove)};

        public override void Enter(PlayerRoot playerRoot)
        {
            playerRoot.CachedComponents.Get<Animator>().Play("PlayerRollTest");
            playerRoot.StartCoroutine(DashCoroutine(playerRoot.CachedComponents.Get<Rigidbody2D>()));
        }
        
        
        private IEnumerator DashCoroutine(Rigidbody2D rigidbody2D)
        {
            StartDash();
            {
                var dashMove = rigidbody2D.position + rigidbody2D.velocity;
                
                var dashStopTime = Time.time + 0.5f;
                
                while (Time.time < dashStopTime)
                {
                    rigidbody2D.MovePosition
                            (Vector2.MoveTowards(rigidbody2D.position, dashMove, Time.deltaTime * 30));
                    
                    yield return new WaitForFixedUpdate();
                }
            }
            ReleaseDash(rigidbody2D);

            yield return new WaitForSeconds(0.1f);
        }
        
        private void StartDash()
        {
            InProcess = true;
        }

        private void ReleaseDash(Rigidbody2D rigidbody2D)
        {
            InProcess = false;
            rigidbody2D.velocity = Vector2.zero;
        }
    }
}