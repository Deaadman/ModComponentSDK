#if UNITY_EDITOR
using ModComponent.SDK.Components;
using System.IO;
using UnityEditor;
using UnityEngine;

public class PrefabPostProcessor : AssetPostprocessor
{
    private static void OnPostprocessAllAssets(
        string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string assetPath in importedAssets)
        {
            if (Path.GetExtension(assetPath).Equals(".prefab"))
            {
                CreateGearAssetForPrefab(assetPath);
            }
        }
    }

    private static void CreateGearAssetForPrefab(string prefabPath)
    {
        string gearAssetFolderPath = "Assets/_ModComponent/DataAssets/GearItems/CurrentUnityProject";
        if (!Directory.Exists(gearAssetFolderPath))
        {
            Directory.CreateDirectory(gearAssetFolderPath);
        }

        string prefabName = Path.GetFileNameWithoutExtension(prefabPath);
        string gearAssetPath = gearAssetFolderPath + "/" + prefabName + ".asset";

        if (File.Exists(gearAssetPath))
        {
            return;
        }

        Texture2D icon = FindIconForPrefab(prefabName);
        DataGearAsset asset = ScriptableObject.CreateInstance<DataGearAsset>();
        asset.Name = prefabName;
        asset.Icon = icon;

        AssetDatabase.CreateAsset(asset, gearAssetPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private static Texture2D FindIconForPrefab(string prefabName)
    {
        string iconName = prefabName.StartsWith("GEAR_") ? prefabName[5..] : prefabName;
        string iconSearchName = "ico_GearItem__" + iconName;

        string[] guids = AssetDatabase.FindAssets(iconSearchName + " t:texture2D");
        foreach (string guid in guids)
        {
            string iconPath = AssetDatabase.GUIDToAssetPath(guid);
            Texture2D icon = AssetDatabase.LoadAssetAtPath<Texture2D>(iconPath);
            if (icon != null)
            {
                SetTextureCompression(iconPath, false);
                return icon;
            }
        }
        return null;
    }

    private static void SetTextureCompression(string path, bool isCompressed)
    {
        TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
        if (textureImporter != null)
        {
            textureImporter.textureCompression = isCompressed ? TextureImporterCompression.Compressed : TextureImporterCompression.Uncompressed;
            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
        }
    }
}
#endif