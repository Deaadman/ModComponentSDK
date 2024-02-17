#if UNITY_EDITOR
using ModComponent.Editor.SDK;
using ModComponent.SDK.Components;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace ModComponent.SDK
{
    internal struct UpdateInfo
    {
        public string CurrentVersion;
        public string LatestVersion;
        public string LatestVersionChanges;
        public string PackageName;

        public UpdateInfo(string currentVersion, string latestVersion, string changes, string packageName)
        {
            CurrentVersion = currentVersion;
            LatestVersion = latestVersion;
            LatestVersionChanges = changes;
            PackageName = packageName;
        }
    }

    internal class AutoUpdater
    {
        private static readonly string ModComponentVersion = ModComponentSDK.SDK_VERSION;
        private static string LatestModComponentVersion;
        private static string LatestExampleModVersion;
        private static string LatestVersionChanges;
        private static readonly Queue<UpdateInfo> updateQueue = new();
        private static bool isUpdateWindowOpen = false;

        private static void AddToUpdateQueue(UpdateInfo updateInfo)
        {
            updateQueue.Enqueue(updateInfo);
            ShowNextUpdateWindow();
        }

        private static async Task DownloadAndImportUnityPackage(string downloadUrl, string localPath)
        {
            using var client = new WebClient();
            TaskCompletionSource<bool> tcs = new();

            client.DownloadFileCompleted += (sender, e) =>
            {
                if (e.Error == null && !e.Cancelled)
                {
                    AssetDatabase.ImportPackage(localPath, true);
                    tcs.SetResult(true);
                }
                else
                {
                    tcs.SetException(e.Error ?? new Exception("Download was cancelled."));
                }
            };

            client.DownloadFileAsync(new Uri(downloadUrl), localPath);
            await tcs.Task;
        }

        private static async Task<bool> FetchLatestReleaseInfoAsync(string repoPath, string currentVersion, string packageName)
        {
            try
            {
                using var client = new WebClient();
                string url = $"https://api.github.com/repos/{repoPath}/releases/latest";
                client.Headers.Add("User-Agent", "Unity web request");
                string json = await client.DownloadStringTaskAsync(new Uri(url));
                var jsonObject = JObject.Parse(json);
                string latestVersion = jsonObject["tag_name"].ToString();

                if (latestVersion != currentVersion)
                {
                    LatestVersionChanges = jsonObject["body"].ToString();
                    if (packageName == "ModComponent SDK")
                    {
                        LatestModComponentVersion = latestVersion;
                    }
                    else
                    {
                        LatestExampleModVersion = latestVersion;
                    }
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }

        private static string GetInstalledExampleModVersion()
        {
            var exampleModAsset = AssetDatabase.LoadAssetAtPath<ModDefinition>("Assets/_ModComponent/ExampleMod/ExampleMod.asset");
            return exampleModAsset != null ? exampleModAsset.Version : null;
        }

        internal static async Task<bool> InitializeUpdateCheck(string updateType)
        {
            string currentVersion = updateType == "modcomponent" ? ModComponentVersion : GetInstalledExampleModVersion();
            string repoPath = updateType == "modcomponent" ? "Deaadman/ModComponentSDK" : "Deaadman/ExampleModSDK";
            string packageName = updateType == "modcomponent" ? "ModComponent SDK" : "Example Mod";

            bool updateAvailable = await FetchLatestReleaseInfoAsync(repoPath, currentVersion, packageName);
            if (updateAvailable)
            {
                string latestVersion = updateType == "modcomponent" ? LatestModComponentVersion : LatestExampleModVersion;
                UpdateInfo updateInfo = new(currentVersion, latestVersion, LatestVersionChanges, packageName);
                AddToUpdateQueue(updateInfo);
            }

            return updateAvailable;
        }

        private static void OpenUpdateWindow(UpdateInfo updateInfo)
        {
            isUpdateWindowOpen = true;
            EditorAutoUpdater.Init(updateInfo.CurrentVersion, updateInfo.LatestVersion, updateInfo.LatestVersionChanges, updateInfo.PackageName,
                () =>
                {
                    isUpdateWindowOpen = false;
                    ShowNextUpdateWindow();
                });
        }

        internal static async Task<bool> PromptAndInstallExampleModIfNeeded()
        {
            string exampleModPath = "Assets/_ModComponent/ExampleMod";
            if (Directory.Exists(exampleModPath))
            {
                return true;
            }

            bool installExampleMod = EditorUtility.DisplayDialog(
                "Example Mod Package Installer",
                "The Example Mod is not installed. Would you like to install it now?",
                "Install", "Skip");

            if (installExampleMod)
            {
                string tempFilePath = Path.Combine(Path.GetTempPath(), "ExampleMod.unitypackage");
                string downloadUrl = "https://github.com/Deadman/ExampleModSDK/releases/latest/download/ExampleMod.unitypackage";
                await DownloadAndImportUnityPackage(downloadUrl, tempFilePath);
            }
            return false;
        }


        private static void ShowNextUpdateWindow()
        {
            if (!isUpdateWindowOpen && updateQueue.Count > 0)
            {
                EditorApplication.update += TryOpenNextUpdateWindow;
            }
        }

        private static void TryOpenNextUpdateWindow()
        {
            if (!isUpdateWindowOpen && updateQueue.Count > 0)
            {
                var updateInfo = updateQueue.Dequeue();
                OpenUpdateWindow(updateInfo);
                EditorApplication.update -= TryOpenNextUpdateWindow;
            }
        }

        internal static void UpdateExampleMod(string latestVersion)
        {
            string tempFilePath = Path.Combine(Path.GetTempPath(), "ExampleMod.unitypackage");
            DownloadAndImportUnityPackage("https://github.com/Deadman/ExampleModSDK/releases/latest/download/ExampleMod.unitypackage", tempFilePath);
        }

        internal static void UpdatePackage(string packageName, string latestVersion)
        {
            string manifestPath = Path.Combine(Application.dataPath, "..", "Packages", "manifest.json");
            if (!File.Exists(manifestPath))
            {
                Debug.LogError("manifest.json not found");
                return;
            }

            string manifestContent = File.ReadAllText(manifestPath);
            var manifestJson = JObject.Parse(manifestContent);
            JToken packageToken = manifestJson["dependencies"][packageName];

            if (packageToken == null)
            {
                Debug.LogError($"{packageName} not found in manifest.json");
                return;
            }

            manifestJson["dependencies"][packageName] = latestVersion;
            File.WriteAllText(manifestPath, manifestJson.ToString());
            AssetDatabase.Refresh();
            Debug.Log($"{packageName} updated successfully.");
        }
    }
}
#endif