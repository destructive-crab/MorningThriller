using Cysharp.Threading.Tasks;
using UnityEngine;

namespace destructive_code.LevelGeneration.CameraManagement
{
    [RequireComponent(typeof(Animator))]
    public sealed class Transition : DepressedBehaviour
    {
        private Animator animator;

        private int EnableAnimation = Animator.StringToHash("TransitionEnable");
        private int DisableAnimation = Animator.StringToHash("TransitionEnable");

        public static Transition Instance { get; private set; }
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            
            animator = GetComponent<Animator>();   
        }

        public async UniTask Enable()
        {
            animator.SetBool("Enabled", true);

            await UniTask.WaitForSeconds(0.33f);
            
            
        }

        public void Disable()
        {
            animator.SetBool("Enabled", false);
        }
    }
}