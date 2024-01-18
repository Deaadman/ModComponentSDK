using Deadman.ModComponent.ModManager;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Deadman.ModComponent.UserInterface
{
    public class ModManagerUserInterface : EditorWindow
    {
        private ModManagerTreeView modTreeView;
        private TreeViewState treeViewState;
        private Mod selectedMod;

        [MenuItem("Mod Component/Mod Manager")]
        public static void ShowWindow()
        {
            GetWindow<ModManagerUserInterface>("Mod Manager");
        }

        private void OnEnable()
        {
            treeViewState = new TreeViewState();
            RefreshModTreeView();
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Refresh"))
            {
                RefreshModTreeView();
            }

            string exportButtonLabel = selectedMod != null ? $"Export {selectedMod.Name}" : "Select a Mod to Export";
            EditorGUI.BeginDisabledGroup(selectedMod == null);
            if (GUILayout.Button(exportButtonLabel))
            {
                string path = EditorUtility.SaveFilePanel("Save Mod Component", "", selectedMod.Name, "modcomponent");
                if (!string.IsNullOrEmpty(path))
                {
                    ModManager.ModManager.ExportModAsModComponent(selectedMod, path);
                }
            }
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.EndHorizontal();

            modTreeView?.OnGUI(new Rect(0, 25, position.width, position.height - 50));
        }

        private void ModSelected(Mod mod)
        {
            selectedMod = mod;
        }

        private void RefreshModTreeView()
        {
            modTreeView = new ModManagerTreeView(treeViewState, ModManager.ModManager.GetAllMods());
            modTreeView.OnItemSelected += ModSelected;
        }
    }
}