#if UNITY_EDITOR
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
            GUILayout.Space(10);
            GUILayout.Label("To get started, check out the below links:", ModComponentEditorStyles.CenteredLabel);
            DrawButtonWithLink("Quick Start Guide", "");
            DrawButtonWithLink("Documentation", "https://github.com/dommrogers/ModComponent/blob/master/docs/index.md");
            GUILayout.Space(10);
            DrawSectionWithLinks("General Information / Links", new Dictionary<string, string>
            {
                { "Changelog", "https://github.com/Deaadman/ModComponentSDK/releases" },
                { "Discord Server", "https://discord.gg/2mnXAZfGXQ" }
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