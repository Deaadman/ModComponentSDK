#if UNITY_EDITOR
using ModComponent.SDK;
using UnityEditor;
using UnityEngine;

namespace ModComponent.Editor.SDK
{
    internal class EditorValidateUpdates : EditorValidateBase
    {
        private CheckStatus exampleModStatus = CheckStatus.Pending;
        private CheckStatus projectUpdateStatus = CheckStatus.Pending;

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
            DrawStatus("ModComponent SDK", ref projectUpdateStatus);

            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
        }

        private async void StartUpdateCheck()
        {
            exampleModStatus = CheckStatus.Checking;
            bool isExampleModInstalled = UnityPackageInstaller.IsPackageInstalled();
            if (isExampleModInstalled)
            {
                bool exampleModUpdateAvailable = await UnityPackageInstaller.CheckForUpdates();
                exampleModStatus = exampleModUpdateAvailable ? CheckStatus.Waiting : CheckStatus.Success; 
                // Despite logic working correctly for checking for updating, the status doesn't reflect it properly.
            }
            else
            {
                exampleModStatus = CheckStatus.Failed;
            }

            projectUpdateStatus = CheckStatus.Checking;
            bool projectUpdateAvailable = await AutoUpdater.InitializeUpdateCheck();
            projectUpdateStatus = projectUpdateAvailable ? CheckStatus.Waiting : CheckStatus.Success;
        }

        protected override string GetStatusMessage(string baseLabel, CheckStatus status)
        {
            return status switch
            {
                CheckStatus.Checking => $"Checking {baseLabel} for updates...",
                CheckStatus.Success => $"{baseLabel} is up to date.",
                CheckStatus.Failed => $"Update check for {baseLabel} failed.",
                CheckStatus.Pending => $"{baseLabel} update status not checked.",
                CheckStatus.Waiting => $"New update available for {baseLabel}.",
                _ => baseLabel,
            };
        }
    }
}
#endif