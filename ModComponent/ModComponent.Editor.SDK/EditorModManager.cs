#if UNITY_EDITOR
using ModComponent.SDK.Components;
using ModComponent.SDK;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace ModComponent.Editor.SDK
{
    internal class EditorModManager : EditorWindow
    {
        private EditorModManagerTreeView modTreeView;
        private TreeViewState treeViewState;
        private ModDefinition selectedMod;

        [MenuItem("ModComponent SDK/Mod Manager (" + ModComponentSDK.SDK_VERSION + ")", false, 0)]
        internal static void ShowWindow()
        {
            GetWindow<EditorModManager>("Mod Manager");
        }

        private void OnEnable()
        {
            treeViewState = new TreeViewState();
            RefreshModTreeView();
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button(new GUIContent("Refresh", "Reload the Mod Manager")))
            {
                RefreshModTreeView();
            }

            if (GUILayout.Button(new GUIContent("Docs", "Open the ModComponent SDK Documentation in your default web browser.")))
            {
                Application.OpenURL("https://github.com/Deaadman/ModComponentSDK/wiki");
            }

            string exportButtonLabel = selectedMod != null ? $"Export {selectedMod.Name}" : "Select a Mod to Export";
            EditorGUI.BeginDisabledGroup(selectedMod == null);

            if (GUILayout.Button(new GUIContent(exportButtonLabel, "Export the selected Mod Definition into a .modcomponent")))
            {
                string path = EditorUtility.SaveFilePanel("Save Mod Component", "", selectedMod.Name, "modcomponent");
                if (!string.IsNullOrEmpty(path))
                {
                    ModManager.ExportModAsModComponent(selectedMod, path);
                }
            }

            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();

            modTreeView?.OnGUI(new Rect(0, 25, position.width, position.height - 50));
        }

        private void ModSelected(ModDefinition modDefinition)
        {
            selectedMod = modDefinition;
        }

        private void RefreshModTreeView()
        {
            modTreeView = new EditorModManagerTreeView(treeViewState, ModManager.GetAllMods());
            modTreeView.OnItemSelected += ModSelected;
        }
    }
}
#endif