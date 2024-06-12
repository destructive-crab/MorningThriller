using destructive_code.Level.GUIWindows;
using destructive_code.Scenes;

public sealed class ReturnToPreviousButton : GUIWindow
{
    public void ReturnToPrevious()
        => SceneSwitcher.CurrentScene.SceneGUI.BackToPrevious();
}
