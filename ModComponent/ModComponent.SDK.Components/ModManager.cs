#if UNITY_EDITOR
using ModComponent.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;
using CompressionLevel = System.IO.Compression.CompressionLevel;

namespace ModComponent.SDK.Components
{
    internal class ModManager
    {
        internal const string ModAssetsPath = "Assets/_ModComponent";
        private const string AssetBundlesPath = ".\\AssetBundles\\";

        internal static void AddAssetsToAddressablesGroup(UnityEngine.Object[] assets, AddressableAssetGroup group)
        {
            var settings = AddressableAssetSettingsDefaultObject.Settings;

            foreach (var asset in assets)
            {
                string assetPath = AssetDatabase.GetAssetPath(asset);
                string guid = AssetDatabase.AssetPathToGUID(assetPath);
                var entry = settings.CreateOrMoveEntry(guid, group);
                entry.address = Path.GetFileNameWithoutExtension(assetPath);
            }
        }

        internal static void ClearAssetBundlesDirectory(string directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                ClearDirectory(directoryPath);
            }
            else
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        internal static void ClearDirectory(string directoryPath, string excludeExtension = null)
        {
            foreach (var file in Directory.GetFiles(directoryPath))
            {
                FileInfo fileInfo = new(file);
                if (excludeExtension == null || !fileInfo.Extension.Equals(excludeExtension, StringComparison.OrdinalIgnoreCase))
                {
                    File.Delete(file);
                }
            }

            foreach (var dir in Directory.GetDirectories(directoryPath))
            {
                Directory.Delete(dir, true);
            }
        }

        internal static List<ModDefinition> GetAllMods()
        {
            List<ModDefinition> modDefinitions = new();

            string[] guids = AssetDatabase.FindAssets("t:ModDefinition", new[] { ModAssetsPath });
            foreach (string guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                ModDefinition modDefinition = AssetDatabase.LoadAssetAtPath<ModDefinition>(assetPath);
                if (modDefinition != null)
                {
                    modDefinitions.Add(modDefinition);
                }
            }

            return modDefinitions;
        }

        internal static void ClearModFolder(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                ClearDirectory(folderPath, ".asset");
            }
            else
            {
                Directory.CreateDirectory(folderPath);
            }
        }

        internal static void CopyAssetBundlesToFolder(string bundlesFolderPath)
        {
            foreach (var file in Directory.EnumerateFiles(AssetBundlesPath))
            {
                if (!file.EndsWith(".hash"))
                {
                    string sanitizedFileName = FileUtilities.SanitizeFileName(Path.GetFileName(file), true);
                    string destFile = Path.Combine(bundlesFolderPath, sanitizedFileName);
                    File.Copy(file, destFile, true);
                }
            }
        }

        internal static void CreateBuildInfoJson(ModDefinition modDefinition, string filePath)
        {
            var buildInfo = new ModDefinition.BuildInfo
            {
                Name = modDefinition.Name,
                Author = modDefinition.Author,
                Version = modDefinition.Version,
                Requires = modDefinition.RequiredMods,
                RequiresDLC = modDefinition.RequiresDLC
            };

            string json = JsonUtility.ToJson(buildInfo, true);
            File.WriteAllText(filePath, json);
        }

        internal static void ExportLocalization(ModDefinition modDefinition, string modFolderPath)
        {
            if (modDefinition.localization != null)
            {
                var localizationData = ExtractLocalizationData(modDefinition);
                string json = JsonConvert.SerializeObject(localizationData, Formatting.Indented);
                SaveLocalizationJson(modFolderPath, json);
            }
        }

        internal static void ExportModAsModComponent(ModDefinition modDefinition, string outputPath)
        {
            var group = AddressablesManager.CreatePackedAssetsGroup(FileUtilities.SanitizeFileName(modDefinition.Name));
            AddressablesManager.ConfigureDefaultAddressableSettings(modDefinition.Name);

            AddAssetsToAddressablesGroup(modDefinition.Items, group);
            AddAssetsToAddressablesGroup(modDefinition.Icons, group);

            ClearAssetBundlesDirectory(AssetBundlesPath);
            AddressableAssetSettings.BuildPlayerContent();

            string modFolderPath = PrepareModFolder(modDefinition);
            CreateBuildInfoJson(modDefinition, Path.Combine(modFolderPath, "BuildInfo.json"));
            ExportLocalization(modDefinition, modFolderPath);
            SerializeModComponentToJson(modDefinition, modFolderPath);

            string modComponentPath = outputPath ?? $"{modDefinition.Name}.modcomponent";
            PackageModComponent(modFolderPath, modComponentPath);
        }

        internal static void PackageModComponent(string modFolderPath, string modComponentPath)
        {
            if (File.Exists(modComponentPath))
            {
                File.Delete(modComponentPath);
            }

            using var zip = ZipFile.Open(modComponentPath, ZipArchiveMode.Create);
            DirectoryInfo di = new(modFolderPath);
            foreach (FileInfo file in di.GetFiles("*", SearchOption.AllDirectories))
            {
                if (file.Extension != ".meta" && file.Extension != ".asset")
                {
                    string fullFilePath = Path.GetFullPath(file.FullName);
                    string relativePath = FileUtilities.SanitizeFileName(fullFilePath[(di.FullName.Length + 1)..], true).Replace("\\", "/");
                    zip.CreateEntryFromFile(fullFilePath, relativePath, CompressionLevel.Optimal);
                }
            }
        }

        internal static string PrepareModFolder(ModDefinition modDefinition)
        {
            string modFolderPath = Path.Combine(ModAssetsPath, modDefinition.Name);
            ClearModFolder(modFolderPath);

            string bundlesFolderPath = Path.Combine(modFolderPath, "bundle");
            Directory.CreateDirectory(bundlesFolderPath);
            CopyAssetBundlesToFolder(bundlesFolderPath);

            return modFolderPath;
        }

        internal static void SaveLocalizationJson(string modFolderPath, string json)
        {
            string localizationFolderPath = Path.Combine(modFolderPath, "localizations");
            Directory.CreateDirectory(localizationFolderPath);
            File.WriteAllText(Path.Combine(localizationFolderPath, "Localization.json"), json);
        }

        internal static void SerializeModComponentToJson(ModDefinition modDefinition, string modFolderPath)
        {
            string autoMappedFolderPath = Path.Combine(modFolderPath, "auto-mapped");
            Directory.CreateDirectory(autoMappedFolderPath);

            foreach (var prefab in modDefinition.Items)
            {
                SerializePrefab(prefab, autoMappedFolderPath);
            }
        }

        internal static void SerializePrefab(GameObject prefab, string folderPath)
        {
            var componentData = new Dictionary<string, object>();

            foreach (var component in prefab.GetComponents<MonoBehaviour>())
            {
                var serializedComponent = DataSerializer.SerializeComponent(component);
                if (serializedComponent != null)
                {
                    string componentTypeName = component.GetType().Name;
                    componentData[componentTypeName] = serializedComponent;
                }
            }

            string json = JsonConvert.SerializeObject(componentData, Formatting.Indented);
            string jsonFileName = prefab.name.StartsWith("GEAR_") ? prefab.name[5..] : prefab.name;
            string jsonFilePath = Path.Combine(folderPath, jsonFileName + ".json");
            File.WriteAllText(jsonFilePath, json);
        }

        private static Dictionary<string, Dictionary<string, string>> ExtractLocalizationData(ModDefinition modDefinition)
        {
            var localizationData = new Dictionary<string, Dictionary<string, string>>();

            foreach (var entry in modDefinition.localization.localizationEntries)
            {
                var translations = new Dictionary<string, string>
                {
                    { "English", entry.languages.English },
                    { "German", entry.languages.German },
                    { "Russian", entry.languages.Russian },
                    { "French (France)", entry.languages.French },
                    { "Japanese", entry.languages.Japanese },
                    { "Korean", entry.languages.Korean },
                    { "Simplified Chinese", entry.languages.SimplifiedChinese },
                    { "Swedish", entry.languages.Swedish },
                    { "Traditional Chinese", entry.languages.TraditionalChinese },
                    { "Turkish", entry.languages.Turkish },
                    { "Norwegian", entry.languages.Norwegian },
                    { "Spanish (Spain)", entry.languages.Spanish },
                    { "Portuguese (Portugal)", entry.languages.PortuguesePortugal },
                    { "Portuguese (Brazil)", entry.languages.PortugueseBrazil },
                    { "Dutch", entry.languages.Dutch },
                    { "Finnish", entry.languages.Finnish },
                    { "Italian", entry.languages.Italian },
                    { "Polish", entry.languages.Polish },
                    { "Ukrainian", entry.languages.Ukrainian }
                };

                localizationData.Add(entry.localizationKey, translations);
            }

            return localizationData;
        }
    }
}
#endif