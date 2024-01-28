#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace ModComponent.Editor.SDK
{
    public abstract class EditorValidateBase : EditorWindow
    {
        protected enum CheckStatus { Pending, Checking, Success, Failed, Waiting }

        protected abstract string GetStatusMessage(string baseLabel, CheckStatus status);

        protected void DrawStatus(string baseLabel, ref CheckStatus status)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            GUIStyle iconStyle = new() { alignment = TextAnchor.MiddleLeft };
            GUIStyle labelStyle = new(GUI.skin.label) { alignment = TextAnchor.MiddleLeft };

            switch (status)
            {
                case CheckStatus.Checking:
                    GUILayout.Label(EditorGUIUtility.IconContent("RotateTool"), iconStyle, GUILayout.Width(20), GUILayout.Height(20));
                    break;
                case CheckStatus.Success:
                    GUILayout.Label(EditorGUIUtility.IconContent("TestPassed"), iconStyle, GUILayout.Width(20), GUILayout.Height(20));
                    break;
                case CheckStatus.Failed:
                    GUILayout.Label(EditorGUIUtility.IconContent("TestFailed"), iconStyle, GUILayout.Width(20), GUILayout.Height(20));
                    break;
                case CheckStatus.Waiting:
                    GUILayout.Label(EditorGUIUtility.IconContent("d_console.warnicon"), iconStyle, GUILayout.Width(20), GUILayout.Height(20));
                    break;
            }

            string statusMessage = GetStatusMessage(baseLabel, status);
            GUILayout.Label(statusMessage, labelStyle);

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
    }
}
#endif