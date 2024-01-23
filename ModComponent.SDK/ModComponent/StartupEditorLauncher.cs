using ModComponent.Editor;
using UnityEditor;

namespace ModComponent
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