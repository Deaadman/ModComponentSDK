using ModComponent.Utilities;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Build;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;

namespace ModComponent
{
    public class AddressablesManager
    {
        public static AddressableAssetGroup CreatePackedAssetsGroup(string groupName)
        {
            var settings = AddressableAssetSettingsDefaultObject.Settings != null ? AddressableAssetSettingsDefaultObject.Settings : AddressableAssetSettings.Create(AddressableAssetSettingsDefaultObject.kDefaultConfigFolder,
                                                           AddressableAssetSettingsDefaultObject.kDefaultConfigAssetName,
                                                           true,
                                                           true);

            var existingGroup = settings.FindGroup(groupName);
            if (existingGroup != null)
            {
                return existingGroup;
            }

            var group = settings.CreateGroup(groupName, false, false, true, null, typeof(BundledAssetGroupSchema));

            var schema = group.GetSchema<BundledAssetGroupSchema>();
            if (schema != null)
            {
                schema.BundleNaming = BundledAssetGroupSchema.BundleNamingStyle.NoHash;
            }

            return group;
        }

        public static void ConfigureDefaultAddressableSettings(string modName)
        {
            var settings = AddressableAssetSettingsDefaultObject.Settings != null ? AddressableAssetSettingsDefaultObject.Settings : AddressableAssetSettingsDefaultObject.Settings =
                           AddressableAssetSettings.Create(AddressableAssetSettingsDefaultObject.kDefaultConfigFolder,
                                                           AddressableAssetSettingsDefaultObject.kDefaultConfigAssetName,
                                                           true,
                                                           true);

            settings.OverridePlayerVersion = FileUtility.SanitizeFileName(modName);

            settings.ShaderBundleNaming = ShaderBundleNaming.Custom;
            settings.ShaderBundleCustomNaming = FileUtility.SanitizeFileName(modName);

            settings.BuildRemoteCatalog = true;
            settings.RemoteCatalogBuildPath.SetVariableByName(settings, AddressableAssetSettings.kLocalBuildPath);
            settings.RemoteCatalogLoadPath.SetVariableByName(settings, AddressableAssetSettings.kLocalLoadPath);
        }

        [InitializeOnLoadMethod]
        internal static void SetCustomLocalBuildPath()
        {
            var settings = AddressableAssetSettingsDefaultObject.Settings;
            if (settings == null)
            {
                return;
            }

            var profileSettings = settings.profileSettings;
            var profileId = settings.activeProfileId;

            var pathVar = profileSettings.GetValueByName(profileId, AddressableAssetSettings.kLocalBuildPath);
            if (pathVar == null)
            {
                profileSettings.CreateValue(AddressableAssetSettings.kLocalBuildPath, @".\AssetBundles\");
                profileSettings.CreateValue(AddressableAssetSettings.kLocalLoadPath, @".\AssetBundles\");
            }
            else
            {
                profileSettings.SetValue(profileId, AddressableAssetSettings.kLocalBuildPath, @".\AssetBundles\");
                profileSettings.SetValue(profileId, AddressableAssetSettings.kLocalLoadPath, @".\AssetBundles\");
            }

            EditorUtility.SetDirty(settings);
            AssetDatabase.SaveAssets();
        }
    }
}