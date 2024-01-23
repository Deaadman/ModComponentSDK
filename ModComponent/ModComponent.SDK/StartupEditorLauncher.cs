#if UNITY_EDITOR
using UnityEditor;

namespace ModComponent.SDK
{
    [InitializeOnLoad]
    public class StartupEditorLauncher
    {
        static StartupEditorLauncher()
        {
            EditorApplication.delayCall += ShowStartupEditor;
        }

        private static void ShowStartupEditor()
        {
            if (!SessionState.GetBool("ShownStartupEditor", false))
            {
                StartupEditor startupEditor = EditorWindow.GetWindow<StartupEditor>("ModComponent SDK");
                startupEditor.Show();
                SessionState.SetBool("ShownStartupEditor", true);
            }
        }
    }
}
#endif