#if UNITY_EDITOR
using ModComponent.Behaviours;
using ModComponent.Components;
using ModComponent.SDK;
using ModComponent.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using UnityEditor;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets;
using UnityEngine;
using CompressionLevel = System.IO.Compression.CompressionLevel;

namespace ModComponent.ModManager
{
    internal class ModManager
    {
        internal const string ModAssetsPath = "Assets/_ModComponent";
        private const string AssetBundlesPath = ".\\AssetBundles\\";

        private static void AddAssetsToAddressablesGroup(UnityEngine.Object[] assets, AddressableAssetGroup group)
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

        private static void ClearAssetBundlesDirectory(string directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                var files = Directory.GetFiles(directoryPath);
                foreach (var file in files)
                {
                    File.Delete(file);
                }

                var directories = Directory.GetDirectories(directoryPath);
                foreach (var dir in directories)
                {
                    Directory.Delete(dir, true);
                }
            }
            else
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        private static void ClearModFolder(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                foreach (var file in Directory.GetFiles(folderPath))
                {
                    FileInfo fileInfo = new(file);
                    if (!fileInfo.Extension.Equals(".asset", StringComparison.OrdinalIgnoreCase))
                    {
                        File.Delete(file);
                    }
                }

                foreach (var dir in Directory.GetDirectories(folderPath))
                {
                    _ = new DirectoryInfo(dir);
                    Directory.Delete(dir, true);
                }
            }
            else
            {
                Directory.CreateDirectory(folderPath);
            }
        }

        private static void CreateBuildInfoJson(Mod mod, string filePath)
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

                string json = JsonConvert.SerializeObject(localizationData, Formatting.Indented);

                string localizationFolderPath = Path.Combine(modFolderPath, "localizations");
                Directory.CreateDirectory(localizationFolderPath);

                File.WriteAllText(Path.Combine(localizationFolderPath, "Localization.json"), json);
            }
        }

        internal static string ExportModAsModComponent(Mod mod, string outputPath)
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

            return modComponentPath;
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

        private static void PackageModComponent(string modFolderPath, string modComponentPath)
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

        private static string PrepareModFolder(Mod mod)
        {
            string modFolderPath = Path.Combine(ModAssetsPath, mod.Name);

            ClearModFolder(modFolderPath);

            string bundlesFolderPath = Path.Combine(modFolderPath, "bundle");
            Directory.CreateDirectory(bundlesFolderPath);

            foreach (var file in Directory.EnumerateFiles(AssetBundlesPath))
            {
                if (!file.EndsWith(".hash"))
                {
                    string sanitizedFileName = FileUtility.SanitizeFileName(Path.GetFileName(file), true);
                    string destFile = Path.Combine(bundlesFolderPath, sanitizedFileName);
                    File.Copy(file, destFile, true);
                }
            }

            return modFolderPath;
        }

        private static void SerializeModComponentToJson(Mod mod, string modFolderPath)
        {
            string autoMappedFolderPath = Path.Combine(modFolderPath, "auto-mapped");
            Directory.CreateDirectory(autoMappedFolderPath);

            foreach (var prefab in mod.Items)
            {
                var componentData = new Dictionary<string, object>();

                if (prefab.TryGetComponent<ModGenericComponent>(out var modGenericComponent))
                {
                    componentData.Add("ModGenericComponent", SerializeUtility.SerializeComponent(modGenericComponent));
                }

                if (prefab.TryGetComponent<ModStackableBehaviour>(out var modStackableBehaviour))
                {
                    componentData.Add("ModStackableBehaviour", SerializeUtility.SerializeComponent(modStackableBehaviour));
                }

                string json = JsonConvert.SerializeObject(componentData, Formatting.Indented);
                string jsonFileName = prefab.name.StartsWith("GEAR_") ? prefab.name[5..] : prefab.name;
                string jsonFilePath = Path.Combine(autoMappedFolderPath, jsonFileName + ".json");
                File.WriteAllText(jsonFilePath, json);
            }
        }
    }
}
#endif