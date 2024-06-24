using Internal.Enemies;
using UnityEditor.Animations;
using UnityEngine;

namespace destructive_code.Player
{
    [RequireComponent(typeof(Animator))]
    public sealed class AnimatorSwitcher : DepressedBehaviour
    {
        [SerializeField] private AnimatorController up;
        [SerializeField] private AnimatorController down;
        [SerializeField] private AnimatorController side;

        private Animator animator;
        private Rigidbody2D rigidbody2D;
        private Flipper flipper;
        
        private void Start()
        {
            animator = GetComponent<Animator>();
            flipper = GetComponentInParent<Flipper>();
            rigidbody2D = GetComponentInParent<Rigidbody2D>();
        }

        private void Update()
        {
            if (rigidbody2D.velocity.y > 0)
            {
                animator.runtimeAnimatorController = up;
            }
            if (rigidbody2D.velocity.y < 0)
            {
                animator.runtimeAnimatorController = down;
            }
            if (rigidbody2D.velocity.y == 0 && rigidbody2D.velocity.x != 0)
            {
                animator.runtimeAnimatorController = side;
                
                if(rigidbody2D.velocity.x > 0)
                    flipper.FlipToRight();
                else if(rigidbody2D.velocity.x < 0)
                    flipper.FlipToLeft();
            }
        }
    }
}