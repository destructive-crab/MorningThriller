using UnityEngine;

namespace destructive_code.LevelGeneration.PlayerCode
{
    public sealed class Player : DepressedBehaviour
    {
        Rigidbody2D body;

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