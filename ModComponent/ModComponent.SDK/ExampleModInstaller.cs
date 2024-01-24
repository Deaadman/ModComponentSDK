using UnityEditor;

public class ExampleModInstaller
{
    private const string ExampleModPrefKey = "HasExampleModBeenPrompted";

    [InitializeOnLoadMethod]
    private static void OnProjectLoadedInEditor()
    {
        if (!EditorPrefs.GetBool(ExampleModPrefKey, false))
        {
            bool shouldInstallExampleMod = EditorUtility.DisplayDialog(
                "Example Mod Package Installer",
                "Would you like to install the Example Mod package?",
                "Yes",
                "No"
            );

            if (shouldInstallExampleMod)
            {
                string pathToExampleMod = "Packages/com.deadman.modcomponent.sdk/ModComponent/Assets/ExampleMod.unitypackage";
                AssetDatabase.ImportPackage(pathToExampleMod, false);
            }

            EditorPrefs.SetBool(ExampleModPrefKey, true);
        }
    }
}