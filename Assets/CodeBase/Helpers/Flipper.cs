using UnityEngine;

namespace Internal.Enemies
{
    public sealed class Flipper : MonoBehaviour
    {
        [field: SerializeField] public bool FaceToRight { get; private set; } = true;
        [SerializeField] private SpriteRenderer spriteRenderer;
        
        public void FlipAtDirection(float x)
        {
            if(x > 0)
                FlipToRight();
            else 
                FlipToLeft();
        }

        public void FlipTo(Transform transform) => FlipTo(transform.position);
        
        public void FlipTo(Vector2 position)
        {
            if (position.x > gameObject.transform.position.x)
            {
                FlipToRight();   
            }
            else
            {
                FlipToLeft();
            }
        }
        
        public void FlipToOpposite()
        {
            if(FaceToRight)
                FlipToLeft();
            else
                FlipToRight();
        }

        public void FlipToRight()
        {
            if (!FaceToRight)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                FaceToRight = true;
            }
        }
        
        public void FlipToLeft()
        {
            if (FaceToRight)
            {
                transform.localScale = new Vector3(1, 1, 1);
                FaceToRight = false;
            }
        }
    }
}