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
        SerializedProperty requiredModsProperty;
        SerializedProperty itemsProperty;
        SerializedProperty iconsProperty;
        SerializedProperty localizationProperty;

        protected override Tab[] GetTabs()
        {
            return new[] { Tab.Common };
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            requiredModsProperty = serializedObject.FindProperty("RequiredMods");
            itemsProperty = serializedObject.FindProperty("Items");
            iconsProperty = serializedObject.FindProperty("Icons");
            localizationProperty = serializedObject.FindProperty("dataLocalization");
        }

        private void DrawCustomFields()
        {
            ModDefinition modDefinition = (ModDefinition)target;

            GUILayout.BeginVertical(ModComponentEditorStyles.BackgroundBox);
            DrawCustomHeading("Required Properties");

            EditorGUI.BeginDisabledGroup(true);
            modDefinition.Name = EditorGUILayout.TextField(new GUIContent("Name", "The name of this ModComponent."), modDefinition.Name);
            modDefinition.Author = EditorGUILayout.TextField(new GUIContent("Author", "The name of the author which created this ModComponent."), modDefinition.Author);
            EditorGUILayout.PropertyField(localizationProperty, new GUIContent("Localization", "Settings related to the localization of this mod."));
            EditorGUI.EndDisabledGroup();

            modDefinition.Version = EditorGUILayout.TextField(new GUIContent("Version", "The current version of this ModComponent."), modDefinition.Version);

            EditorGUILayout.Space(10);
            DrawCustomHeading("Required Dependencies (Optional)");
            EditorGUILayout.PropertyField(requiredModsProperty, new GUIContent("Required Mods", "A list of mods that this ModComponent depends on to work. Leave blank if it depends on none."), true);
            modDefinition.RequiresDLC = EditorGUILayout.Toggle(new GUIContent("Requires DLC", "Does this ModComponent require TFTFT (Tales of The Far Territory) to work?"), modDefinition.RequiresDLC);

            EditorGUILayout.Space(10);
            DrawCustomHeading("Content Properties");
            EditorGUILayout.PropertyField(itemsProperty, new GUIContent("GEAR Item Prefabs", "Drag and drop any 'GEAR_' Prefabs into this field."), true);
            EditorGUILayout.PropertyField(iconsProperty, new GUIContent("Inventory, Crafting & Paperdoll Textures", "Drag and drop any of those textures into this field. DO NOT drag textures used on your materials into this field."), true);

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