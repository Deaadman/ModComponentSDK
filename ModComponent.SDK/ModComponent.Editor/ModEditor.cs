using ModComponent.ModManager;
using UnityEngine;
using UnityEditor;

namespace ModComponent.Editor
{
    [CustomEditor(typeof(Mod))]
    public class ModEditor : UnityEditor.Editor
    {
        SerializedProperty requiredModsProperty;
        SerializedProperty itemsProperty;
        SerializedProperty iconsProperty;

        private void OnEnable()
        {
            requiredModsProperty = serializedObject.FindProperty("RequiredMods");
            itemsProperty = serializedObject.FindProperty("Items");
            iconsProperty = serializedObject.FindProperty("Icons");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            Mod mod = (Mod)target;

            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.TextField("Name", mod.Name);
            EditorGUILayout.TextField("Author", mod.Author);
            EditorGUI.EndDisabledGroup();

            mod.Version = EditorGUILayout.TextField("Version", mod.Version);
            EditorGUILayout.PropertyField(requiredModsProperty, new GUIContent("Required Mods"), true);
            mod.RequiresDLC = EditorGUILayout.Toggle("Requires DLC", mod.RequiresDLC);

            EditorGUILayout.PropertyField(itemsProperty, new GUIContent("Gear Items"), true);
            EditorGUILayout.PropertyField(iconsProperty, new GUIContent("Icons"), true);

            if (GUI.changed)
            {
                EditorUtility.SetDirty(mod);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}