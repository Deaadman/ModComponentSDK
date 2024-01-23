#if UNITY_EDITOR
using ModComponent.ModManager;
using System.IO;
using UnityEditor;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets;
using ModComponent.Utilities;

namespace ModComponent.SDK
{
    internal class DeletionProcessor : AssetModificationProcessor
    {
        private static AssetDeleteResult OnWillDeleteAsset(string assetPath, RemoveAssetOptions option)
        {
            if (Path.GetExtension(assetPath) == ".asset")
            {
                Mod mod = AssetDatabase.LoadAssetAtPath<Mod>(assetPath);
                if (mod != null)
                {
                    HandleModDeletion(mod);
                }
            }

            return AssetDeleteResult.DidNotDelete;
        }

        private static void HandleModDeletion(Mod mod)
        {
            string modFolderPath = Path.Combine(ModManager.ModManager.ModAssetsPath, mod.Name);
            if (Directory.Exists(modFolderPath))
            {
                Directory.Delete(modFolderPath, true);
            }

            AddressableAssetGroup group = AddressableAssetSettingsDefaultObject.Settings.FindGroup(FileUtility.SanitizeFileName(mod.Name));
            if (group != null)
            {
                AddressableAssetSettingsDefaultObject.Settings.RemoveGroup(group);
            }
        }
    }
}
#endif