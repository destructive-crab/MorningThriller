using destructive_code.PlayerCodeBase;
using destructive_code.PlayerCodeBase.CommonStates;
using destructive_code.PlayerCodeBase.Standard;
using destructive_code.Scenes;
using UnityEngine;

namespace destructive_code.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Fish : PlayerRoot
    {
        protected override void InitializeComponents()
        {
            SetLevelScene(Game.LevelScene);
            
            CachedComponents
                .Register(GetComponent<Rigidbody2D>())
                .Register(GetComponentInChildren<SpriteRenderer>())
                .Register(GetComponentInChildren<Animator>());
        }

        protected override void InitializeStates()
        {
            OverrideFactory<PlayerMove>(new StandardStateFactory<StandardPlayerMove>());
            OverrideFactory<PlayerIdle>(new StandardStateFactory<StandardPlayerIdle>());
        }

        protected override void FinishInitialization() { }

        protected override void UpdateInheritor()
        {
            if (LevelScene.InputService.GetMovement() != Vector2.zero && !IsMoving)
            {
                EnterState(GetFactory<PlayerMove>().GetState());
            }
            else if(LevelScene.InputService.GetMovement() == Vector2.zero && !IsIdle)
            {
                EnterState(GetFactory<PlayerIdle>().GetState());   
            }
            
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if(ExtensionContainer.HasExtension<RainbowExtension>())
                {
                    ExtensionContainer.RemoveExtension<RainbowExtension>();
                }
                else
                {
                    ExtensionContainer.AddExtension(new RainbowExtension());
                }
            }
        }
    }
}