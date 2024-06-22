using System;

namespace destructive_code.PlayerCodeBase
{
    public abstract class PlayerState
    {
        public abstract Type[] CanBeEnteredFrom { get; }
        public abstract bool AllowRepeats { get; }
        
        public virtual void Enter(PlayerRoot playerRoot) { }
        public virtual void Update(PlayerRoot playerRoot) { }
        public virtual void FixedUpdate(PlayerRoot playerRoot) { }
        public virtual void Exit(PlayerRoot playerRoot) { }
    }
}