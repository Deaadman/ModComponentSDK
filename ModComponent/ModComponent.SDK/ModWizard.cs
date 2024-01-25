#if UNITY_EDITOR
using ModComponent.Editor.SDK;
using ModComponent.SDK.Components;
using ModComponent.Utilities;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace ModComponent.SDK
{
    internal class ModWizard : ScriptableWizard
    {
        [Tooltip("The name of this Mod Definition.")]
        public string modName = "My Mod";

        [Tooltip("The author of this Mod Definition.")]
        public string modAuthor = "Author";
        public static readonly string modFolderName = "_ModComponent";

        [MenuItem("ModComponent SDK/New Mod Definition", false, 20)]
        private static void CreateWizard()
        {
            DisplayWizard<ModWizard>("New Mod Definition", "Create");
        }

        void OnWizardCreate()
        {
            ModDefinition modDefinition = ModDefinition.CreateModDefinition(modName, modAuthor);

            string mainFolderPath = Path.Combine("Assets", modFolderName);
            if (!Directory.Exists(mainFolderPath))
            {
                Directory.CreateDirectory(mainFolderPath);
            }

            string specificFolderPath = Path.Combine(mainFolderPath, modName);
            if (!Directory.Exists(specificFolderPath))
            {
                Directory.CreateDirectory(specificFolderPath);
            }

            string assetPath = Path.Combine(specificFolderPath, FileUtilities.SanitizeFileName(modName) + ".asset");
            AssetDatabase.CreateAsset(modDefinition, assetPath);

            string localizationAssetPath = Path.Combine(specificFolderPath, FileUtilities.SanitizeFileName(modName) + "Localization.asset");
            Localization localization = CreateInstance<Localization>();
            AssetDatabase.CreateAsset(localization, localizationAssetPath);

            modDefinition.localization = localization;

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        void OnWizardUpdate()
        {
            helpString = "Fill out the below details to create a new Mod Definition asset.\nThis lays down the groundwork for creating a custom ModComponent.";

            errorString = "";
            if (string.IsNullOrEmpty(modName))
            {
                errorString += "Missing Name!";
            }
            if (string.IsNullOrEmpty(modAuthor))
            {
                if (!string.IsNullOrEmpty(errorString))
                    errorString += "\n";
                errorString += "Missing Author!";
            }

            isValid = string.IsNullOrEmpty(errorString);
        }
    }
}
#endif