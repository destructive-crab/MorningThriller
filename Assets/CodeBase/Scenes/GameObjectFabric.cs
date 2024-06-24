using System;
using destructive_code.LevelGeneration;
using destructive_code.GameObjects;
using UnityEngine;
using Object = UnityEngine.Object;

namespace destructive_code.Scenes
{
    public class GameObjectFabric
    {
        public event Action<Object> OnInstantiated;
        public event Action<Object> OnDestroyed;
        public event Action<GameObject> OnInstantiatedGO;
        public event Action<GameObject> OnDestroyedGO;
        
        public virtual TObject Instantiate<TObject>(TObject original, Vector3 position)
            where TObject : Object
        {
            return Instantiate(original, position, Quaternion.identity, null);
        }
        
        public virtual TObject Instantiate<TObject>(TObject original, Vector3 position, Transform parent)
            where TObject : Object
        {
            return Instantiate(original, position, Quaternion.identity, parent);
        }
        
        public virtual  TObject Instantiate<TObject>(TObject original, Vector3 position, Quaternion rotation)
            where TObject : MonoBehaviour
        {
            return Instantiate(original, position, rotation, null);
        }

        public virtual TObject Instantiate<TObject>(TObject original, Vector3 position, Quaternion rotation,
            Transform parent)
            where TObject : Object
        {
            var instance = GameObject.Instantiate(original, position, rotation, parent);

            OnInstantiated?.Invoke(instance);

            if (instance is GameObject gameObject)
            {
                OnInstantiatedGO?.Invoke(gameObject);
                
                InitLevelScene(gameObject);
            }
            else if (instance is Component component)
            {
                OnInstantiatedGO?.Invoke(component.gameObject);
                
                if(component is DepressedBehaviour depressedBehaviour)
                {
                    depressedBehaviour.SetLevelScene(Game.LevelScene);
                }
                else
                {
                    InitLevelScene(component.gameObject);
                }
            }

            return instance;

            void InitLevelScene(GameObject gameObject)
            {
                var behaviours = gameObject.GetComponents<DepressedBehaviour>();

                foreach (var behaviour in behaviours)
                {
                    behaviour.SetLevelScene(Game.LevelScene);
                }
            }
        }

        public virtual async void Destroy(GameObject gameObject)
        {
            if (gameObject.TryGetComponent(out DestroyDelay destroyDelay))
            {
                var behaviours = gameObject.GetComponents<DepressedBehaviour>();
                
                for (int i = 0; i < behaviours.Length; i++)
                {
                    behaviours[i].WillBeDestroyed();
                }
                
                await destroyDelay.Destroy();
            }

            OnDestroyed?.Invoke(gameObject);
            GameObject.Destroy(gameObject);
        }
    }
}