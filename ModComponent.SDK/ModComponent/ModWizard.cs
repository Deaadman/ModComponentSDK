using Deadman.ModComponent.ModManager;
using System.IO;
using UnityEditor;

namespace Deadman.ModComponent
{
    public class ModWizard : ScriptableWizard
    {
        public string modName = "My Mod";
        public string modAuthor = "Author";
        public static readonly string modFolderName = "_ModComponent";

        [MenuItem("Mod Component/New Mod", false, 42160)]
        static void CreateWizard()
        {
            DisplayWizard<ModWizard>("Create Mod", "Create");
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
            helpString = "Create a new Mod for Mod Component.\nFill in the details to create a new Mod asset.";

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