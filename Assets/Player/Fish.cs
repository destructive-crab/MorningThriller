using MorningThriller.PlayerLogic;
using MorningThriller.PlayerLogic.CommonStates;
using MorningThriller.PlayerLogic.Standard;
using MothDIed;
using UnityEngine;

namespace MorningThriller.Player
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

        protected override void InitializeExtensions()
        {
            ExtensionContainer.AddExtension(new AddStuffToData());
        }

        protected override void InitializeStates()
        {
            OverrideFactory<PlayerMove>(new StandardStateFactory<StandardPlayerMove>());
            OverrideFactory<PlayerIdle>(new StandardStateFactory<StandardPlayerIdle>());
            OverrideFactory<PlayerRoll>(new StandardStateFactory<StandardPlayerRoll>());
        }

        protected override void FinishInitialization()
        {
            LevelScene.InputService.OnRoll += OnRoll;
        }

        private void OnRoll()
        {
            EnterState(GetFactory<PlayerRoll>().GetState());
        }

        protected override void UpdateInheritor()
        {
            if (LevelScene.InputService.GetMovement() != Vector2.zero && !IsMoving && (IsRolling == null || !IsRolling.InProcess))
            {
                EnterState(GetFactory<PlayerMove>().GetState());
            }
            else if(LevelScene.InputService.GetMovement() == Vector2.zero && !IsIdle && (IsRolling == null || !IsRolling.InProcess))
            {
                EnterState(GetFactory<PlayerIdle>().GetState());   
            }
        }
    }
}