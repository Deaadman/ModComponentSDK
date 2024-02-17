#if UNITY_EDITOR
using ModComponent.SDK;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace ModComponent.Editor.SDK
{
    internal class EditorValidateUpdates : EditorValidateBase
    {
        private CheckStatus exampleModStatus = CheckStatus.Pending;
        private CheckStatus modComponentStatus = CheckStatus.Pending;

        [MenuItem("ModComponent SDK/Check for Updates...", false, 50)]
        internal static void ShowWindow()
        {
            var window = GetWindow<EditorValidateUpdates>("Update Checker");
            window.minSize = new Vector2(300, 300);
            window.StartUpdateCheck();
        }

        private void OnGUI()
        {
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();

            DrawStatus("Example Mod", ref exampleModStatus);
            DrawStatus("ModComponent SDK", ref modComponentStatus);

            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
        }

        private async void StartUpdateCheck()
        {
            exampleModStatus = CheckStatus.Checking;

            bool isExampleModInstalled = Directory.Exists("Assets/_ModComponent/ExampleMod");
            if (!isExampleModInstalled)
            {
                exampleModStatus = CheckStatus.Failed;
            }
            else
            {
                bool exampleModUpdateAvailable = await AutoUpdater.InitializeUpdateCheck("examplemod");
                exampleModStatus = exampleModUpdateAvailable ? CheckStatus.Waiting : CheckStatus.Success;
            }

            modComponentStatus = CheckStatus.Checking;
            bool modComponentUpdateAvailable = await AutoUpdater.InitializeUpdateCheck("modcomponent");
            modComponentStatus = modComponentUpdateAvailable ? CheckStatus.Waiting : CheckStatus.Success;
        }

        protected override string GetStatusMessage(string baseLabel, CheckStatus status)
        {
            return status switch
            {
                CheckStatus.Checking => $"Checking {baseLabel} for updates...",
                CheckStatus.Success => $"{baseLabel} is up to date.",
                CheckStatus.Failed => $"{baseLabel} isn't installed.",
                CheckStatus.Pending => $"{baseLabel} update status not checked.",
                CheckStatus.Waiting => $"New update available for {baseLabel}.",
                _ => baseLabel,
            };
        }
    }
}
#endif