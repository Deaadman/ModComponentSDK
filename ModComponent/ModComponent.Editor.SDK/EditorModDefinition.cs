#if UNITY_EDITOR
using ModComponent.Editor.API;
using ModComponent.SDK.Components;
using UnityEngine;
using UnityEditor;

namespace ModComponent.Editor.SDK
{
    [CustomEditor(typeof(ModDefinition))]
    internal class EditorModDefinition : EditorBase
    {
        SerializedProperty requiredMods, items, icons;

        protected override Tab[] GetTabs() => new[] { Tab.Common };

        protected override void OnEnable()
        {
            base.OnEnable();
            requiredMods = serializedObject.FindProperty("RequiredMods");
            items = serializedObject.FindProperty("Items");
            icons = serializedObject.FindProperty("Icons");
        }

        void DrawCustomFields()
        {
            ModDefinition modDefinition = (ModDefinition)target;

            GUILayout.BeginVertical(ModComponentEditorStyles.BackgroundBox);
            DrawCustomHeading("General Properties");

            EditorGUI.BeginDisabledGroup(true);
            modDefinition.Name = EditorGUILayout.TextField(new GUIContent("Name", "The name of this ModComponent."), modDefinition.Name);
            modDefinition.Author = EditorGUILayout.TextField(new GUIContent("Author", "The name of the author which created this ModComponent."), modDefinition.Author);
            EditorGUI.EndDisabledGroup();

            modDefinition.Version = EditorGUILayout.TextField(new GUIContent("Version", "The current version of this ModComponent."), modDefinition.Version);

            EditorGUILayout.Space(10);
            DrawCustomHeading("Required Dependencies (Optional)");
            EditorGUILayout.PropertyField(requiredMods, new GUIContent("Required Mods", "A list of mods that this ModComponent depends on to work. Leave blank if it depends on none."), true);
            modDefinition.RequiresDLC = EditorGUILayout.Toggle(new GUIContent("Requires DLC", "Does this ModComponent require TFTFT (Tales of The Far Territory) to work?"), modDefinition.RequiresDLC);

            EditorGUILayout.Space(10);
            DrawCustomHeading("Content Properties");
            EditorGUILayout.PropertyField(items, new GUIContent("GEAR Item Prefabs", "Drag and drop any 'GEAR_' Prefabs into this field."), true);
            EditorGUILayout.PropertyField(icons, new GUIContent("Inventory, Crafting & Paperdoll Textures", "Drag and drop any of those textures into this field. DO NOT drag textures used on your materials into this field."), true);

            GUILayout.EndVertical();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawCustomFields();

            if (GUI.changed)
            {
                EditorUtility.SetDirty(target);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif