#if UNITY_EDITOR
using ModComponent.Editor.SDK;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using UnityEditor;
using UnityEngine;

namespace ModComponent.SDK
{
    internal class AutoUpdater
    {
        private static readonly string currentVersion = Information.SDK_VERSION;
        private static string latestVersion;
        private static string latestVersionChanges;

        [InitializeOnLoadMethod]
        internal static void Initialize()
        {
            FetchLatestReleaseInfo();
            if (latestVersion != null && latestVersion != currentVersion)
            {
                EditorApplication.update += OpenUpdateWindow;
            }
            else
            {
                Debug.Log("ModComponent SDK is up-to-date.");
            }
        }

        internal static void OpenUpdateWindow()
        {
            EditorApplication.update -= OpenUpdateWindow;
            AutoUpdaterEditor.Init(currentVersion, latestVersion, latestVersionChanges);
        }

        internal static void UpdatePackage(string latestVersion)
        {
            string manifestPath = Path.Combine(Application.dataPath, "..", "Packages", "manifest.json");
            if (File.Exists(manifestPath))
            {
                string manifestContent = File.ReadAllText(manifestPath);
                JObject manifestJson = JObject.Parse(manifestContent);

                string packageName = "modcomponent.sdk";
                if (manifestJson["dependencies"][packageName] != null)
                {
                    manifestJson["dependencies"][packageName] = latestVersion;
                    File.WriteAllText(manifestPath, manifestJson.ToString());
                    AssetDatabase.Refresh();
                    Debug.Log("Package updated successfully.");
                }
                else
                {
                    Debug.LogError("Package not found in manifest.json");
                }
            }
            else
            {
                Debug.LogError("manifest.json not found");
            }
        }

        private static void FetchLatestReleaseInfo()
        {
            try
            {
                using WebClient client = new();
                string url = "https://api.github.com/repos/Deaadman/ModComponentSDK/releases/latest";
                client.Headers.Add("User-Agent", "Unity web request");
                string json = client.DownloadString(url);
                JObject jsonObject = JObject.Parse(json);

                latestVersion = jsonObject["tag_name"].ToString();
                latestVersionChanges = jsonObject["body"].ToString();
            }
            catch
            {
                latestVersion = null;
                latestVersionChanges = null;
            }
        }
    }
}
#endif