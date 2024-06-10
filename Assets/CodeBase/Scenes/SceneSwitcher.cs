using System;
using destructive_code.SceneLoader;    

namespace destructive_code.Scenes
{
    public static class SceneSwitcher
    {
        public static bool IsSceneLoaded { get; private set; }
        public static Scene CurrentScene { get; private set; }
 
        public static event Action<Scene> OnSceneStartedLoading; //prev
        public static event Action<Scene, Scene> OnSceneLoaded; //prev/new

        private static readonly AsyncSceneLoader Loader = new();
        
        public static void SwitchTo<TScene>(TScene scene)
            where TScene : Scene
        {
            IsSceneLoaded = false;
            OnSceneStartedLoading?.Invoke(CurrentScene);

            CurrentScene?.Dispose();
            CurrentScene = scene;
            
            scene.BeforeSceneLoaded();
            Loader.LoadScene(scene.GetSceneName(), () => Complete(scene));
        }

        private static void Complete<TScene>(TScene scene) 
            where TScene : Scene
        {
            Scene prevScene = CurrentScene;

            scene.OnLoaded();

            IsSceneLoaded = true;
            OnSceneLoaded?.Invoke(prevScene, scene);
        }

        public static void Tick()
        {
            CurrentScene?.Tick();
        }
    }
}