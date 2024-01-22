using UnityEditor;

namespace ModComponent
{
    [InitializeOnLoad]
    public class WelcomeWindowLauncher
    {
        static WelcomeWindowLauncher()
        {
            EditorApplication.delayCall += ShowWelcomeWindowOnStart;
        }

        private static void ShowWelcomeWindowOnStart()
        {
            if (!SessionState.GetBool("ShownWelcomeWindow", false))
            {
                WelcomeMessageWindow window = EditorWindow.GetWindow<WelcomeMessageWindow>("ModComponent SDK");
                window.Show();
                SessionState.SetBool("ShownWelcomeWindow", true);
            }
        }
    }
}