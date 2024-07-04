using MorningThriller.LevelGeneration;
using MothDIed.ExtensionSystem;
using MothDIed.ServiceLocators;
using UnityEngine;

namespace MothDIed
{
    public abstract class DepressedBehaviour : MonoBehaviour
    {
        public readonly ServiceLocator<Component> CachedComponents = new();
        protected LevelScene LevelScene { get; private set; }
        
        //If use extension system
        public readonly ExtensionContainer ExtensionContainer = new ExtensionContainer();

        public void SetLevelScene(LevelScene scene) => LevelScene ??= scene;
    }
}