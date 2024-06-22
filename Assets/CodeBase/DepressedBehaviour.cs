using destructive_code.LevelGeneration;
using UnityEngine;

namespace destructive_code
{
    public class DepressedBehaviour : MonoBehaviour
    {
        protected LevelScene LevelScene { get; private set; }

        public void SetLevelScene(LevelScene scene) => LevelScene ??= scene;

        public virtual void WillBeDestroyed() { }
    }
}