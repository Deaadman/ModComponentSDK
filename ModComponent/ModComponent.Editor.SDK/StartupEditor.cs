#if UNITY_EDITOR
using ModComponent.Editor;
using UnityEditor;
using UnityEngine;

namespace ModComponent.SDK
{
    public class StartupEditor : EditorWindow
    {
        private void OnGUI()
        {
            GUILayout.BeginVertical();
            EditorContentDrawer.DrawWelcomeContent();
            GUILayout.EndVertical();
        }
    }
}
#endif