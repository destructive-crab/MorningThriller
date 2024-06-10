using UnityEngine;

namespace destrutctive_code.GUIWindows
{
    public abstract class GUIWindow : MonoBehaviour
    {
        protected virtual void OnThisOpened(GUILayer layer) {}
        protected virtual void OnThisClosed(GUILayer layer) {}
        
        protected virtual void OnOtherOpened(GUILayer layer, GUIWindow window) {}
    }
}