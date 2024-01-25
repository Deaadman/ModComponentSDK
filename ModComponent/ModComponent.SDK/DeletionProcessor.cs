#if UNITY_EDITOR
using ModComponent.SDK.Components;
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
                ModDefinition modDefinition = AssetDatabase.LoadAssetAtPath<ModDefinition>(assetPath);
                if (modDefinition != null)
                {
                    HandleModDeletion(modDefinition);
                }
            }

            return AssetDeleteResult.DidNotDelete;
        }

        private static void HandleModDeletion(ModDefinition modDefinition)
        {
            string modFolderPath = Path.Combine(ModManager.ModAssetsPath, modDefinition.Name);
            if (Directory.Exists(modFolderPath))
            {
                Directory.Delete(modFolderPath, true);
            }

            AddressableAssetGroup group = AddressableAssetSettingsDefaultObject.Settings.FindGroup(FileUtilities.SanitizeFileName(modDefinition.Name));
            if (group != null)
            {
                AddressableAssetSettingsDefaultObject.Settings.RemoveGroup(group);
            }
        }
    }
}
#endif