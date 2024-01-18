using UnityEngine;
using UnityEditor;
using Deadman.ModComponent.ModManager;

namespace Deadman.ModComponent
{
    [CustomEditor(typeof(Mod))]
    public class ModEditor : Editor
    {
        SerializedProperty itemsProperty;
        SerializedProperty iconsProperty;

        private void OnEnable()
        {
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