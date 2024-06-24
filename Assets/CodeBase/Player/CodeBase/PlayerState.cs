using System;
using UnityEngine;

namespace destructive_code.PlayerCodeBase
{
    public abstract class PlayerState
    {
        //override only one of this two getters
        public virtual Type[] CanBeEnteredOnlyFrom => Array.Empty<Type>();  
        public virtual Type[] CannotBeEnteredFrom => Array.Empty<Type>();

        public virtual bool CanEnterFrom(Type type)
        {
            if (type == this.GetType() && !AllowRepeats)
            {
                return false;
            }

            for (int i = 0; i < CanBeEnteredOnlyFrom.Length; i++)
            {
                if (!(CanBeEnteredOnlyFrom[i] == type || type.IsSubclassOf(CanBeEnteredOnlyFrom[i])))
                    return false;
            }
            for (int i = 0; i < CannotBeEnteredFrom.Length; i++)
            {
                if (CannotBeEnteredFrom[i] == type || type.IsSubclassOf(CanBeEnteredOnlyFrom[i]))
                    return false;
            }

            return true;
        }
        
        public abstract bool AllowRepeats { get; }
        
        public virtual void Enter(PlayerRoot playerRoot) { }
        public virtual void Update(PlayerRoot playerRoot) { }
        public virtual void FixedUpdate(PlayerRoot playerRoot) { }
        public virtual void Exit(PlayerRoot playerRoot) { }
    }
}