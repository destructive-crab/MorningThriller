using Internal.Enemies;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Serialization;

namespace destructive_code.LevelGeneration.PlayerCode
{
    [RequireComponent(typeof(Flipper))]
    public sealed class Player : DepressedBehaviour
    {
        public AnimatorController side;
        public AnimatorController front;
        public AnimatorController back;
        
        public Rigidbody2D body;

        float horizontal;
        float vertical;

        public float runSpeed = 5.0f;

        void Start ()
        {
            body = GetComponent<Rigidbody2D>(); 
        }

        void Update ()
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical"); 
        }

        private void FixedUpdate()
        {  
            body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
            
            if(body.velocity != Vector2.zero)
            {
                GetComponent<Animator>().Play("Run");

                if (body.velocity.y > 0)
                {
                    GetComponent<Animator>().runtimeAnimatorController = back;
                }
                
                if (body.velocity.y < 0)
                {
                    GetComponent<Animator>().runtimeAnimatorController = front;
                }

                if (body.velocity.y == 0)
                {
                    GetComponent<Animator>().runtimeAnimatorController = side;
                    
                    if (body.velocity.x > 0)
                    {
                        GetComponent<Flipper>().FlipToRight();
                    }
                    else
                    {
                        GetComponent<Flipper>().FlipToLeft();   
                    }
                }
            }
            else
            {
                GetComponent<Animator>().Play("Idle");
            }
            
        }
        
        public void Enable()
        {
            runSpeed = 5.0f;
        }

        public void Disable()
        {
            runSpeed = 0f;
        }
    }
}