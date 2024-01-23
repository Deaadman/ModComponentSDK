using ModComponent.ModManager;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace ModComponent.Editor
{
    public class ModWizard : ScriptableWizard
    {
        [Tooltip("The name of this ModComponent.")]
        public string modName = "My Mod";

        [Tooltip("The author of this ModComponent.")]
        public string modAuthor = "Author";
        public static readonly string modFolderName = "_ModComponent";

        [MenuItem("ModComponent SDK/Create New Mod", false, 100)]
        static void CreateWizard()
        {
            DisplayWizard<ModWizard>("Create New Mod", "Create");
        }

        void OnWizardCreate()
        {
            Mod mod = Mod.CreateMod(modName, modAuthor);

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

            string assetPath = Path.Combine(specificFolderPath, modName + ".asset");
            AssetDatabase.CreateAsset(mod, assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        void OnWizardUpdate()
        {
            helpString = "Fill out the below details to create a new Mod asset.\nThis lays down the groundwork for creating a custom ModComponent.";

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