#if UNITY_EDITOR
using ModComponent.SDK;
using UnityEditor;
using UnityEngine;

namespace ModComponent.Editor.SDK
{
    internal class AutoUpdaterEditor : EditorWindow
    {
        private static string currentVersion;
        private static string latestVersion;
        private static string latestVersionChanges;
        private Vector2 scrollPosition;

        [MenuItem("ModComponent SDK/Check For Update", false, 20)]
        private static void CheckForUpdate()
        {
            AutoUpdater.Initialize();
        }

        internal static void Init(string currentVer, string latestVer, string changes)
        {
            currentVersion = currentVer;
            latestVersion = latestVer;
            latestVersionChanges = ConvertMarkdownToRichText(changes);
            var window = GetWindow<AutoUpdaterEditor>("Auto Updater");
            window.minSize = new Vector2(350, 400);
            window.Show();
        }

        void OnGUI()
        {
            GUIStyle richTextStyle = new(GUI.skin.textArea)
            {
                richText = true
            };

            GUILayout.BeginVertical(ModComponentEditorStyles.BackgroundBox);
            GUILayout.Label("ModComponent SDK Auto Updater", ModComponentEditorStyles.CenteredLabelBold);
            GUILayout.Label("A newer version is available for the ModComponent SDK.", EditorStyles.centeredGreyMiniLabel);
            GUILayout.Space(10);
            GUILayout.Label($"Current Version: {currentVersion}", ModComponentEditorStyles.CenteredLabel);
            GUILayout.Space(5);
            GUILayout.Label($"Latest Version: {latestVersion}", ModComponentEditorStyles.CenteredLabelBold);
            GUILayout.Space(10);

            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.ExpandHeight(true));
            GUILayout.Label("What's New:", EditorStyles.boldLabel);
            GUILayout.TextArea(latestVersionChanges, richTextStyle, GUILayout.ExpandHeight(true));
            GUILayout.EndScrollView();

            GUILayout.Space(10);
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Remind Me Later", GUILayout.Width(120)))
            {
                Close();
            }

            GUILayout.Space(10);

            if (GUILayout.Button("Update Now", GUILayout.Width(120)))
            {
                AutoUpdater.UpdatePackage(latestVersion);
                Close();
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }

        private static string ConvertMarkdownToRichText(string markdown)
        {
            var lines = markdown.Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("##"))
                {
                    lines[i] = "<size=15>" + lines[i][2..].Trim() + "</size>";
                }
            }
            markdown = string.Join("\n", lines);

            markdown = System.Text.RegularExpressions.Regex.Replace(markdown, @"\*\*(.*?)\*\*", "<b>$1</b>");
            markdown = markdown.Replace("`", "");

            return markdown;
        }
    }
}
#endif