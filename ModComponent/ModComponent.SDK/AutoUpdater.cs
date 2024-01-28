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
    internal class AutoUpdater
    {
        private static readonly string CurrentVersion = ModComponentSDK.SDK_VERSION;
        private static string _latestVersion;
        private static string _latestVersionChanges;

        internal static async Task<bool> InitializeUpdateCheck()
        {
            await FetchLatestReleaseInfoAsync();

            if (_latestVersion != null && _latestVersion != CurrentVersion)
            {
                EditorApplication.update += OpenUpdateWindow;
                return true;
            }
            else
            {
                return false;
            }
        }

        private static void OpenUpdateWindow()
        {
            EditorApplication.update -= OpenUpdateWindow;
            EditorAutoUpdater.Init(CurrentVersion, _latestVersion, _latestVersionChanges);
        }

        internal static void UpdatePackage(string latestVersion)
        {
            string manifestPath = Path.Combine(Application.dataPath, "..", "Packages", "manifest.json");

            if (!File.Exists(manifestPath))
            {
                Debug.LogError("manifest.json not found");
                return;
            }

            string manifestContent = File.ReadAllText(manifestPath);
            var manifestJson = JObject.Parse(manifestContent);

            const string packageName = "modcomponent.sdk";
            JToken packageToken = manifestJson["dependencies"][packageName];

            if (packageToken == null)
            {
                Debug.LogError("Package not found in manifest.json");
                return;
            }

            manifestJson["dependencies"][packageName] = latestVersion;
            File.WriteAllText(manifestPath, manifestJson.ToString());
            AssetDatabase.Refresh();
            Debug.Log("Package updated successfully.");
        }

        private static async Task FetchLatestReleaseInfoAsync()
        {
            try
            {
                using var client = new WebClient();
                const string url = "https://api.github.com/repos/Deaadman/ModComponentSDK/releases/latest";
                client.Headers.Add("User-Agent", "Unity web request");

                string json = await client.DownloadStringTaskAsync(new Uri(url));
                var jsonObject = JObject.Parse(json);

                _latestVersion = jsonObject["tag_name"].ToString();
                _latestVersionChanges = jsonObject["body"].ToString();
            }
            catch
            {
                _latestVersion = null;
                _latestVersionChanges = null;
            }
        }
    }
}
#endif