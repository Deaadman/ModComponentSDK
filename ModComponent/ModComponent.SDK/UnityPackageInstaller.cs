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
                    EditorPrefs.SetString(LastUpdateKey, lastModified);
                    return true;
                }
            }
            else
            {
                Debug.LogError("Error checking for updates: " + headRequest.error);
            }

            return false;
        }

        private static async Task DownloadAndInstallPackage()
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
                await DownloadAndInstallPackage();
                return true;
            }

            return false;
        }

        private static Task SendWebRequestAsync(UnityWebRequest webRequest)
        {
            var completionSource = new TaskCompletionSource<object>();
            webRequest.SendWebRequest().completed += _ => completionSource.SetResult(null);
            return completionSource.Task;
        }
    }
}
#endif