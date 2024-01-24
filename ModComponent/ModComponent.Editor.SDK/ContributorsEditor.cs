#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ModComponent.Editor.SDK
{
    internal class ContributorsEditor : EditorWindow
    {
        private readonly Dictionary<string, bool> ContributorFoldouts = new();

        internal static void ShowWindow()
        {
            var contributorsEditor = GetWindow<ContributorsEditor>("MC Contributors");
            contributorsEditor.Show();
        }

        private void OnGUI()
        {
            GUILayout.BeginVertical(ModComponentEditorStyles.BackgroundBox, GUILayout.Width(position.width - 20));

            GUILayout.Label("Contributors of ModComponent / ModComponent SDK", ModComponentEditorStyles.CenteredLabelBold);
            GUILayout.Label("The following people below have contributed to either ModComponent or ModComponent SDK. Click on the profiles to learn more about their contributions and optionally show your appreciation with a donation.", ModComponentEditorStyles.CenteredGreyMiniLabel);
            GUILayout.Space(10);

            GUILayout.Label("Current Contributors", EditorStyles.boldLabel);
            DrawContributor("Deadman", "Current developer and maintainer of the ModComponent SDK. Deadman has played a pivotal role in building and maintaining this project.", "https://github.com/Deaadman/", "https://ko-fi.com/deaadman");
            GUILayout.Space(5);
            DrawContributor("STBlade", "Current developer and maintainer of the ModComponent infrastructure. STBlade's work has been instrumental in making this project possible.", "https://github.com/dommrogers", "https://ko-fi.com/stblade");

            GUILayout.Space(20);
            GUILayout.Label("Retired Contributors", EditorStyles.boldLabel);
            DrawContributor("ds5678", "Successor of WulfMarius, ds5678 took over ModComponent and played a significant role in shaping it into what it is today. His contributions have been invaluable.", "https://github.com/ds5678", "https://paypal.me/ds5678");
            GUILayout.Space(5);
            DrawContributor("WulfMarius", "The original creator of ModComponent and AssetLoader, WulfMarius laid the foundation upon which this project is built. We are incredibly grateful for his hard work.", "https://github.com/WulfMarius", "N/A");

            GUILayout.EndVertical();
        }

        private void DrawContributor(string name, string role, string profileLink, string donateLink)
        {
            if (!ContributorFoldouts.ContainsKey(name))
            {
                ContributorFoldouts[name] = false;
            }

            GUILayout.BeginHorizontal();
            ContributorFoldouts[name] = EditorGUILayout.Foldout(ContributorFoldouts[name], name, true);

            if (GUILayout.Button("Profile", GUILayout.Width(100)))
            {
                Application.OpenURL(profileLink);
            }
            if (GUILayout.Button("Donate", GUILayout.Width(100)))
            {
                Application.OpenURL(donateLink);
            }
            GUILayout.EndHorizontal();

            if (ContributorFoldouts[name])
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(20);
                GUILayout.Label(role, ModComponentEditorStyles.WrappedLabel);
                GUILayout.EndHorizontal();
            }
        }
    }
}
#endif