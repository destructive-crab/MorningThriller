using System.Collections.Generic;
using System;
using MorningThriller.PlayerLogic.CommonStates;
using MothDIed;

namespace MorningThriller.PlayerLogic
{
    public abstract class PlayerRoot : DepressedBehaviour
    {
        public PlayerState CurrentState { get; private set; }

        public bool IsIdle => CurrentState is PlayerIdle;
        public bool IsMoving => CurrentState is PlayerMove;
        public bool IsRolling => CurrentState is PlayerRoll;
        
        private readonly Dictionary<Type, PlayerStateFactory> stateFactories = new Dictionary<Type, PlayerStateFactory>();

        protected abstract void InitializeComponents();
        protected virtual void InitializeExtensions() { }
        protected abstract void InitializeStates();
        protected abstract void FinishInitialization();

        protected virtual void UpdateInheritor() {}
        protected virtual void FixedUpdateInheritor() {}
        protected virtual void OnEnableInheritor() {}
        protected virtual void OnDisableInheritor() {}

        private void OnEnable()
        {
            CurrentState?.Enter(this);
            ExtensionContainer.EnableContainer();
            
            OnEnableInheritor();
        }

        private void OnDisable()
        {
            CurrentState?.Exit(this);
            ExtensionContainer.DisableContainer();
            
            OnDisableInheritor();
        }

        private void Start()
        {
            InitializeComponents();
            InitializeExtensions();
            InitializeStates();
            FinishInitialization();
            
            ExtensionContainer.StartContainer(this);
        }

        private void Update()
        {
            CurrentState?.Update(this);
            ExtensionContainer.UpdateContainer();
            
            UpdateInheritor();
        }

        private void FixedUpdate()
        {
            CurrentState?.FixedUpdate(this);
            ExtensionContainer.FixedUpdateContainer();
            
            FixedUpdateInheritor();
        }

        protected PlayerStateFactory GetFactory<TFactory>()
            where TFactory : PlayerState
        {
            return stateFactories[typeof(TFactory)]; 
        }

        public void OverrideFactory<TFactory>(PlayerStateFactory factory)
            where TFactory : PlayerState
        {
            if(stateFactories.ContainsKey(typeof(TFactory)))
            {
                stateFactories[typeof(TFactory)] = factory;
            }
            else
            {
                stateFactories.Add(typeof(TFactory), factory);   
            }
        }

        public TState Get<TState>()
            where TState : PlayerState
        {
            return CurrentState as TState;
        }

        public bool TryGet<TState>(out TState state)
            where TState : PlayerState
        {
            state = Get<TState>();
            return state != null;
        }
        
        public void EnterState(PlayerState state)
        {
            if (IsAvailable(state))
            {
                CurrentState?.Exit(this);
                CurrentState = state;
                CurrentState.Enter(this);
            }
        }
        
        private bool IsAvailable(PlayerState state)
            => state != null && (CurrentState == null || (CurrentState != null && CurrentState.CanEnterFrom(state.GetType())));
    }
}