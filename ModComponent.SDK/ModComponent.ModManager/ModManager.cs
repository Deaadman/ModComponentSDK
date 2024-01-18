using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using System.IO.Compression;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets;
using Deadman.ModComponent.Components;
using Newtonsoft.Json;

namespace Deadman.ModComponent.ModManager
{
    public class ModManager
    {
        private const string ModAssetsPath = "Assets/_ModComponent";
        private const string AssetBundlesPath = ".\\AssetBundles\\";

        public static List<Mod> GetAllMods()
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

        public static string ExportModAsModComponent(Mod mod, string outputPath)
        {
            var group = AddressablesManager.CreatePackedAssetsGroup(mod.Name);
            AddressablesManager.ConfigureDefaultAddressableSettings(mod.Name);

            AddAssetsToAddressablesGroup(mod.Items, group);
            AddAssetsToAddressablesGroup(mod.Icons, group);

            AddressableAssetSettings.BuildPlayerContent();

            string modFolderPath = PrepareModFolder(mod);

            CreateBuildInfoJson(mod, Path.Combine(modFolderPath, "BuildInfo.json"));

            SerializeModComponentToJson(mod, modFolderPath);

            string modComponentPath = outputPath ?? $"{mod.Name}.modcomponent";
            PackageModComponent(mod, modFolderPath, modComponentPath);

            return modComponentPath;
        }

        private static string PrepareModFolder(Mod mod)
        {
            string modFolderPath = Path.Combine(ModAssetsPath, mod.Name);
            string bundlesFolderPath = Path.Combine(modFolderPath, "bundles").Replace("\\", "/");

            Directory.CreateDirectory(bundlesFolderPath);

            foreach (var file in Directory.EnumerateFiles(AssetBundlesPath))
            {
                if (!file.EndsWith(".hash"))
                {
                    string sanitizedFileName = SanitizeFileName(Path.GetFileName(file));
                    string destFile = Path.Combine(bundlesFolderPath, sanitizedFileName);
                    File.Copy(file, destFile, true);
                }
            }

            return modFolderPath;
        }

        private static void CreateBuildInfoJson(Mod mod, string filePath)
        {
            var buildInfo = new BuildInfo
            {
                Name = mod.Name,
                Author = mod.Author,
                Version = mod.Version
            };

            string json = JsonUtility.ToJson(buildInfo, true);
            File.WriteAllText(filePath, json);
        }

        private struct BuildInfo
        {
            public string Name;
            public string Author;
            public string Version;
        }

        private static void PackageModComponent(Mod mod, string modFolderPath, string modComponentPath)
        {
            if (File.Exists(modComponentPath))
            {
                File.Delete(modComponentPath);
            }

            string modAssetPath = Path.GetFullPath(AssetDatabase.GetAssetPath(mod));

            using var zip = ZipFile.Open(modComponentPath, ZipArchiveMode.Create);
            DirectoryInfo di = new(modFolderPath);
            foreach (FileInfo file in di.GetFiles("*", SearchOption.AllDirectories))
            {
                string fullFilePath = Path.GetFullPath(file.FullName);
                if (file.Extension != ".meta" && fullFilePath != modAssetPath)
                {
                    string relativePath = SanitizeFileName(file.FullName[(di.FullName.Length + 1)..]);
                    zip.CreateEntryFromFile(fullFilePath, relativePath, System.IO.Compression.CompressionLevel.Optimal);
                }
            }
        }

        private static void SerializeModComponentToJson(Mod mod, string modFolderPath)
        {
            string autoMappedFolderPath = Path.Combine(modFolderPath, "auto-mapped"));
            Directory.CreateDirectory(autoMappedFolderPath);

            foreach (var prefab in mod.Items)
            {
                if (prefab.TryGetComponent<ModGenericComponent>(out var modComponent))
                {
                    var modComponentData = new
                    {
                        modComponent.DisplayNameLocalizationId,
                        modComponent.DescriptionLocalizatonId,
                        modComponent.InventoryActionLocalizationId,
                        modComponent.WeightKG,
                        modComponent.DaysToDecay,
                        modComponent.MaxHP,
                        modComponent.InitialCondition,
                        modComponent.InventoryCategory,
                        modComponent.PickUpAudio,
                        modComponent.PutBackAudio,
                        modComponent.StowAudio,
                        modComponent.WornOutAudio,
                        modComponent.InspectOnPickup,
                        modComponent.InspectDistance,
                        InspectAngles = new float[] { modComponent.InspectAngles.x, modComponent.InspectAngles.y, modComponent.InspectAngles.z },
                        InspectOffset = new float[] { modComponent.InspectOffset.x, modComponent.InspectOffset.y, modComponent.InspectOffset.z },
                        InspectScale = new float[] { modComponent.InspectScale.x, modComponent.InspectScale.y, modComponent.InspectScale.z },
                        modComponent.NormalModel,
                        modComponent.InspectModel
                    };

                    var wrappedData = new { ModGenericComponent = modComponentData };

                    string json = JsonConvert.SerializeObject(wrappedData, Formatting.Indented);
                    string jsonFileName = prefab.name.StartsWith("GEAR_") ? prefab.name[5..] : prefab.name;
                    string jsonFilePath = Path.Combine(autoMappedFolderPath, jsonFileName + ".json");
                    File.WriteAllText(jsonFilePath, json);
                }
            }
        }

        private static string SanitizeFileName(string fileName)
        {
            return fileName.Replace(" ", "").ToLower();
        }

        private static void AddAssetsToAddressablesGroup(Object[] assets, AddressableAssetGroup group)
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
    }
}