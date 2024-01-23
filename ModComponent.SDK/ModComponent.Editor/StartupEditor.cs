using ModComponent.Utilities;
using UnityEditor;
using UnityEngine;

namespace ModComponent.Editor
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