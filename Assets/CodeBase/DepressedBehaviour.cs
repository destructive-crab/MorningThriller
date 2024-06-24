using destructive_code.LevelGeneration;
using destructive_code.ExtensionSystem;
using destructive_code.ServiceLocators;
using UnityEngine;

namespace destructive_code
{
    public class DepressedBehaviour : MonoBehaviour
    {
        protected LevelScene LevelScene { get; private set; }

        public readonly LocalServiceLocator<Component> CachedComponents = new LocalServiceLocator<Component>();
        public readonly ExtensionContainer ExtensionContainer = new ExtensionContainer();
        
        public void SetLevelScene(LevelScene scene) => LevelScene ??= scene;
    
        public virtual void WillBeDestroyed() { }
    }
}