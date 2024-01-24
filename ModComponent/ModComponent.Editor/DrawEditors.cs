#if UNITY_EDITOR
using ModComponent.Editor.SDK;
using ModComponent.SDK;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ModComponent.Editor
{
    internal static class EditorContentDrawer
    {
        internal static void DrawWelcomeContent()
        {
            GUILayout.BeginVertical(ModComponentEditorStyles.BackgroundBox);
            GUILayout.Label($"Welcome to the {Information.SDK_NAME}!", ModComponentEditorStyles.CenteredLabelBold);
            GUILayout.Label($"Version {Information.SDK_VERSION}", EditorStyles.centeredGreyMiniLabel);
            GUILayout.Space(5);
            GUILayout.Label($"Supports ModComponent Version {Information.MODCOMPONENT_VERSION}", EditorStyles.centeredGreyMiniLabel);
            GUILayout.Space(10);
            GUILayout.Label("To get started, check out the below links:", ModComponentEditorStyles.CenteredLabel);
            DrawButtonWithLink("Beginner's Guide", "");
            DrawButtonWithLink("Documentation", "https://github.com/Deaadman/ModComponentSDK/wiki");
            GUILayout.Space(10);
            DrawSectionWithLinks("News / Community", new Dictionary<string, string>
            {
                { "Changelog", "https://github.com/Deaadman/ModComponentSDK/releases" },
                { "TLD Modding Discord Server", "https://discord.gg/2mnXAZfGXQ" }
            });
            GUILayout.Space(10);
            GUILayout.Label("Enjoying the SDK? Please consider leaving donations, \n" +
                            "it means a lot to us modders. Thank you.", EditorStyles.centeredGreyMiniLabel);
            if (GUILayout.Button("Contributors / Donations"))
            {
                ContributorsEditor.ShowWindow();
            }
            GUILayout.EndVertical();
        }

        private static void DrawButtonWithLink(string label, string url)
        {
            if (GUILayout.Button(label))
            {
                Application.OpenURL(url);
            }
        }

        private static void DrawSectionWithLinks(string sectionTitle, Dictionary<string, string> links)
        {
            GUILayout.Label(sectionTitle, EditorStyles.centeredGreyMiniLabel);
            foreach (var link in links)
            {
                DrawButtonWithLink(link.Key, link.Value);
            }
        }
    }
}
#endif