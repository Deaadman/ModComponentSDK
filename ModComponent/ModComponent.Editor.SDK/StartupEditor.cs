#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace ModComponent.Editor.SDK
{
    internal class StartupEditor : EditorWindow
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