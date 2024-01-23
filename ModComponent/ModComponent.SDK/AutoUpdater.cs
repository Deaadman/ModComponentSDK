#if UNITY_EDITOR
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using UnityEditor;
using UnityEngine;

namespace ModComponent.SDK
{
    [InitializeOnLoad]
    public class PackageAutoUpdater
    {
        static PackageAutoUpdater()
        {
            EditorApplication.update += CheckForUpdate;
        }

        private static void CheckForUpdate()
        {
            EditorApplication.update -= CheckForUpdate;

            string currentVersion = Information.SDK_VERSION;
            string latestVersion = GetLatestVersionFromServer();

            if (latestVersion != null && latestVersion != currentVersion)
            {
                PromptForUpdate(currentVersion, latestVersion);
            }
            else
            {
                Debug.Log("Your package is up to date.");
            }
        }

        private static void PromptForUpdate(string currentVersion, string latestVersion)
        {
            bool userWantsToUpdate = EditorUtility.DisplayDialog(
                "Update Available",
                $"A new version of the package is available. \n\nCurrent Version: {currentVersion}\nLatest Version: {latestVersion}\n\nWould you like to update?",
                "Yes, Update",
                "No, Not Now");

            if (userWantsToUpdate)
            {
                UpdatePackage(latestVersion);
            }
            else
            {
                return;
            }
        }

        private static void UpdatePackage(string latestVersion)
        {
            string manifestPath = Path.Combine(Application.dataPath, "..", "Packages", "manifest.json");
            if (File.Exists(manifestPath))
            {
                string manifestContent = File.ReadAllText(manifestPath);
                JObject manifestJson = JObject.Parse(manifestContent);

                string packageName = "com.deadman.modcomponent.sdk";
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

        private static string GetLatestVersionFromServer()
        {
            try
            {
                using WebClient client = new();
                string url = "https://raw.githubusercontent.com/Deaadman/ModComponentSDK/release/package.json";
                string json = client.DownloadString(url);
                JObject jsonObject = JObject.Parse(json);
                return jsonObject["version"].ToString();
            }
            catch
            {
                return null;
            }
        }
    }
}
#endif