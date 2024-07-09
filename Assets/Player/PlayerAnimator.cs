using MothDIed.DI;
using MothDIed.ExtensionSystem;
using UnityEngine;

namespace MorningThriller.Player
{
    public class PlayerAnimator : Extension
    {
        [Inject] private Animator animator;
        
        public override void StartExtension()
        {
            animator = Owner.CachedComponents.Get<Animator>();
        }

        public void PlayIdle(float speed)
        {
            animator.speed = speed;
            animator.Play("Idle");
        }

        public void PlayRun(float speed)
        {
            animator.speed = speed;
            animator.Play("Run");
        }
        
        public void PlayRoll(float speed)
        {
            animator.speed = speed;
            animator.Play("Roll");
        }
    }
}