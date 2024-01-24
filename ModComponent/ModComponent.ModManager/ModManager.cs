#if UNITY_EDITOR
using ModComponent.SDK;
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

namespace ModComponent.ModManager
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

        internal static List<Mod> GetAllMods()
        {
            List<Mod> mods = new();

            string[] guids = AssetDatabase.FindAssets("t:Mod", new[] { ModAssetsPath });
            foreach (string guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                Mod mod = AssetDatabase.LoadAssetAtPath<Mod>(assetPath);
                if (mod != null)
                {
                    mods.Add(mod);
                }
            }

            return mods;
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
                    string sanitizedFileName = FileUtility.SanitizeFileName(Path.GetFileName(file), true);
                    string destFile = Path.Combine(bundlesFolderPath, sanitizedFileName);
                    File.Copy(file, destFile, true);
                }
            }
        }

        internal static void CreateBuildInfoJson(Mod mod, string filePath)
        {
            var buildInfo = new Mod.BuildInfo
            {
                Name = mod.Name,
                Author = mod.Author,
                Version = mod.Version,
                Requires = mod.RequiredMods,
                RequiresDLC = mod.RequiresDLC
            };

            string json = JsonUtility.ToJson(buildInfo, true);
            File.WriteAllText(filePath, json);
        }

        internal static void ExportLocalization(Mod mod, string modFolderPath)
        {
            if (mod.localization != null)
            {
                var localizationData = ExtractLocalizationData(mod);
                string json = JsonConvert.SerializeObject(localizationData, Formatting.Indented);
                SaveLocalizationJson(modFolderPath, json);
            }
        }

        internal static void ExportModAsModComponent(Mod mod, string outputPath)
        {
            var group = AddressablesManager.CreatePackedAssetsGroup(FileUtility.SanitizeFileName(mod.Name));
            AddressablesManager.ConfigureDefaultAddressableSettings(mod.Name);

            AddAssetsToAddressablesGroup(mod.Items, group);
            AddAssetsToAddressablesGroup(mod.Icons, group);

            ClearAssetBundlesDirectory(AssetBundlesPath);
            AddressableAssetSettings.BuildPlayerContent();

            string modFolderPath = PrepareModFolder(mod);
            CreateBuildInfoJson(mod, Path.Combine(modFolderPath, "BuildInfo.json"));
            ExportLocalization(mod, modFolderPath);
            SerializeModComponentToJson(mod, modFolderPath);

            string modComponentPath = outputPath ?? $"{mod.Name}.modcomponent";
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
                    string relativePath = FileUtility.SanitizeFileName(fullFilePath[(di.FullName.Length + 1)..], true).Replace("\\", "/");
                    zip.CreateEntryFromFile(fullFilePath, relativePath, CompressionLevel.Optimal);
                }
            }
        }

        internal static string PrepareModFolder(Mod mod)
        {
            string modFolderPath = Path.Combine(ModAssetsPath, mod.Name);
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

        internal static void SerializeModComponentToJson(Mod mod, string modFolderPath)
        {
            string autoMappedFolderPath = Path.Combine(modFolderPath, "auto-mapped");
            Directory.CreateDirectory(autoMappedFolderPath);

            foreach (var prefab in mod.Items)
            {
                SerializePrefab(prefab, autoMappedFolderPath);
            }
        }

        internal static void SerializePrefab(GameObject prefab, string folderPath)
        {
            var componentData = new Dictionary<string, object>();

            foreach (var component in prefab.GetComponents<MonoBehaviour>())
            {
                var serializedComponent = SerializeUtility.SerializeComponent(component);
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

        private static Dictionary<string, Dictionary<string, string>> ExtractLocalizationData(Mod mod)
        {
            var localizationData = new Dictionary<string, Dictionary<string, string>>();

            foreach (var entry in mod.localization.localizationEntries)
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