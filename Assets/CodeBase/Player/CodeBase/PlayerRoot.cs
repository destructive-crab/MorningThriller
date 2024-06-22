using System;
using System.Collections.Generic;
using System.Linq;
using destructive_code.PlayerCodeBase.CommonStates;
using destructive_code.ServiceLocators;
using UnityEngine;

namespace destructive_code.PlayerCodeBase
{
    public abstract class PlayerRoot : DepressedBehaviour
    {
        public PlayerState CurrentState { get; private set; }

        public bool IsIdle => CurrentState is PlayerIdle;
        public bool IsMoving => CurrentState is PlayerMove;
        public bool IsRolling => CurrentState is PlayerRoll;

        public readonly ServiceLocator<Component> CachedComponents = new ServiceLocator<Component>();

        private PlayerStateFactory idleFactory;
        private PlayerStateFactory moveFactory;
        private PlayerStateFactory rollFactory;

        private readonly Dictionary<Type, PlayerStateFactory> stateFactories = new Dictionary<Type, PlayerStateFactory>();

        protected abstract void InitializeComponents();
        protected abstract void InitializeStates();
        protected abstract void FinishInitialization();
        
        protected virtual void UpdateInheritor() {}
        protected virtual void FixedUpdateInheritor() {}
        protected virtual void OnEnableInheritor() {}
        protected virtual void OnDisableInheritor() {}

        private void OnEnable()
        {
            CurrentState?.Enter(this);
            OnEnableInheritor();
        }

        private void OnDisable()
        {
            CurrentState?.Exit(this);
            OnDisableInheritor();
        }

        private void Start()
        {
            InitializeComponents();
            InitializeStates();
            FinishInitialization();
        }

        private void Update()
        {
            CurrentState?.Update(this);
            UpdateInheritor();
        }

        private void FixedUpdate()
        {
            CurrentState?.FixedUpdate(this);
            FixedUpdateInheritor();
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

        public PlayerStateFactory GetFactory<TFactory>()
            where TFactory : PlayerState
        {
            return stateFactories[typeof(TFactory)]; 
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
        {
            return state != null && !(CurrentState != null && CurrentState.GetType() == state.GetType() && CurrentState.AllowRepeats) && (CurrentState == null || state.CanBeEnteredFrom.Contains<Type>(CurrentState.GetType()) ||
                                     state.CanBeEnteredFrom.Length == 0);
        }
    }
}