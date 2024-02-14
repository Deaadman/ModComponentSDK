#if UNITY_EDITOR
using ModComponent.Editor.SDK;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace ModComponent.SDK
{
    // More works need to be done.
    // 1. If an update is found, testing needs to be done for actually installing it.
    // 2. Same goes with the Example Mod, especially auto imported it for the user.
    // 3. To get the current version, we need this to point to the Assets/_ModComponent/ExampleMod/ExampleMod.asset file.
    // 4. Which it will check the version string variable to see if it matches. (We need to somehow lock this field for the ExampleMod only, otherwise users can change it and mess things up)
    internal class AutoUpdater
    {
        private static readonly string ModComponentVersion = ModComponentSDK.SDK_VERSION;
        private static readonly string ExampleModVersion = ModComponentSDK.EXAMPLEMODSDK_VERSION;

        private static string LatestModComponentVersion;
        private static string LatestExampleModVersion;
        private static string LatestVersionChanges;

        internal static async Task<bool> InitializeUpdateCheck(string updateType)
        {
            bool updateAvailable = false;
            switch (updateType)
            {
                case "modcomponent":
                    updateAvailable = await FetchLatestReleaseInfoAsync("Deaadman/ModComponentSDK", ModComponentVersion, "ModComponent SDK");
                    break;
                case "examplemod":
                    updateAvailable = await FetchLatestReleaseInfoAsync("Deaadman/ExampleModSDK", ExampleModVersion, "Example Mod");
                    break;
            }

            if (updateAvailable)
            {
                EditorApplication.update += OpenUpdateWindow;
            }

            return updateAvailable;
        }

        private static void OpenUpdateWindow()
        {
            EditorApplication.update -= OpenUpdateWindow;
            if (LatestModComponentVersion != null)
            {
                EditorAutoUpdater.Init(ModComponentVersion, LatestModComponentVersion, LatestVersionChanges, "ModComponent SDK");
            }
            if (LatestExampleModVersion != null)
            {
                EditorAutoUpdater.Init(ExampleModVersion, LatestExampleModVersion, LatestVersionChanges, "Example Mod");
            }
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
                    else if (packageName == "Example Mod")
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
    }
}
#endif