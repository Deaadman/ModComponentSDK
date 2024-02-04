#if UNITY_EDITOR
using System.IO;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace ModComponent.SDK
{
    internal class UnityPackageInstaller
    {
        private const string ExampleModUrl = "https://modcomponentsdk.s3.ap-southeast-2.amazonaws.com/ExampleMod.unitypackage";
        private const string LastUpdateKey = "ExampleModLastUpdate";
        private const string ExampleModPath = "Assets/_ModComponent/ExampleMod";

        internal static async Task<bool> CheckForUpdates()
        {
            if (!Directory.Exists(ExampleModPath))
            {
                return false;
            }

            string lastUpdate = EditorPrefs.GetString(LastUpdateKey, "");

            using UnityWebRequest headRequest = UnityWebRequest.Head(ExampleModUrl);
            await SendWebRequestAsync(headRequest);

            if (headRequest.result == UnityWebRequest.Result.Success)
            {
                string lastModified = headRequest.GetResponseHeader("last-modified");
                if (lastModified != lastUpdate)
                {
                    bool userWantsToUpdate = EditorUtility.DisplayDialog(
                        "Unity Package Updater",
                        "An update for the Example Mod is available. Would you like to install it now?",
                        "Update",
                        "Skip");

                    if (userWantsToUpdate)
                    {
                        await DownloadAndInstallPackage(lastModified);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                Debug.LogError("Error checking for updates: " + headRequest.error);
            }

            return false;
        }

        private static async Task DownloadAndInstallPackage(string lastModified)
        {
            string localPath = Path.Combine(Application.temporaryCachePath, "ExampleMod.unitypackage");

            using UnityWebRequest webRequest = UnityWebRequest.Get(ExampleModUrl);
            await SendWebRequestAsync(webRequest);

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error downloading Unity package: " + webRequest.error);
            }
            else
            {
                File.WriteAllBytes(localPath, webRequest.downloadHandler.data);
                AssetDatabase.ImportPackage(localPath, false);
                EditorPrefs.SetString(LastUpdateKey, lastModified);
            }
        }

        internal static bool IsPackageInstalled()
        {
            return Directory.Exists(ExampleModPath);
        }

        internal static async Task<bool> PromptPackageInstallation()
        {
            if (Directory.Exists(ExampleModPath))
            {
                return true;
            }

            bool userResponse = EditorUtility.DisplayDialog(
                "Unity Package Installer",
                "Would you like to install the Example Mod to this Unity Project?",
                "Install",
                "Skip");

            if (userResponse)
            {
                string lastModified = await FetchLastModifiedTimestamp();
                if (!string.IsNullOrEmpty(lastModified))
                {
                    await DownloadAndInstallPackage(lastModified);
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        private static Task SendWebRequestAsync(UnityWebRequest webRequest)
        {
            var completionSource = new TaskCompletionSource<object>();
            webRequest.SendWebRequest().completed += _ => completionSource.SetResult(null);
            return completionSource.Task;
        }

        private static async Task<string> FetchLastModifiedTimestamp()
        {
            using UnityWebRequest headRequest = UnityWebRequest.Head(ExampleModUrl);
            await SendWebRequestAsync(headRequest);
            if (headRequest.result == UnityWebRequest.Result.Success)
            {
                return headRequest.GetResponseHeader("last-modified");
            }
            else
            {
                Debug.LogError("Error fetching last-modified timestamp: " + headRequest.error);
                return null;
            }
        }
    }
}
#endif