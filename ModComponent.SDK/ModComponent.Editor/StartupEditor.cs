using UnityEditor;
using UnityEngine;

namespace ModComponent.Editor
{
    public class StartupEditor : EditorWindow
    {
        private void OnGUI()
        {
            GUIStyle style = new(EditorStyles.helpBox)
            {
                margin = new RectOffset(10, 10, 10, 12),
                padding = new RectOffset(10, 10, 10, 12)
            };

            GUIStyle centeredStyleBold = new(GUI.skin.label)
            {
                alignment = TextAnchor.MiddleCenter,
                fontStyle = FontStyle.Bold,
                wordWrap = true
            };

            GUIStyle centeredStyle = new(GUI.skin.label)
            {
                alignment = TextAnchor.MiddleCenter,
                wordWrap = true
            };

            GUILayout.BeginVertical(style, GUILayout.Width(300f - 35f));
            GUILayout.Label($"Welcome to the {ModComponentSDK.SDK_NAME}!", centeredStyleBold);
            GUILayout.Label($"Version {ModComponentSDK.SDK_VERSION}", EditorStyles.centeredGreyMiniLabel);
            GUILayout.Space(10);
            GUILayout.Label($"To get started, check out the below links:", centeredStyle);
            GUILayout.Label("New to modding The Long Dark?", EditorStyles.centeredGreyMiniLabel);
            if (GUILayout.Button("Quick Start Guide"))
            {
                //Application.OpenURL("");
            }
            if (GUILayout.Button("Documentation"))
            {
                Application.OpenURL("https://github.com/dommrogers/ModComponent/blob/master/docs/index.md");
            }

            GUILayout.Space(10);
            GUILayout.Label("General Information / Links", EditorStyles.centeredGreyMiniLabel);
            if (GUILayout.Button("Changelog"))
            {
                Application.OpenURL("https://github.com/Deaadman/ModComponentSDK/releases");
            }
            if(GUILayout.Button("Discord Server"))
            {
                Application.OpenURL("https://discord.gg/2mnXAZfGXQ");
            }
            
            GUILayout.Space(10);
            GUILayout.Label("Enjoying the SDK? Please consider leaving donations, \n" +
                            "it means a lot to us modders. Thank you.", EditorStyles.centeredGreyMiniLabel);
            if (GUILayout.Button("Donate"))
            {
                Application.OpenURL("https://ko-fi.com/deaadman");
            }

            GUILayout.EndVertical();
        }
    }
}