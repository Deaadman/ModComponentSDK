#if UNITY_EDITOR
using ModComponent.ModManager;
using UnityEngine;
using UnityEditor;

namespace ModComponent.SDK
{
    [CustomEditor(typeof(Mod))]
    internal class ModEditor : UnityEditor.Editor
    {
        SerializedProperty requiredModsProperty;
        SerializedProperty itemsProperty;
        SerializedProperty iconsProperty;
        SerializedProperty localizationProperty;

        private void OnEnable()
        {
            requiredModsProperty = serializedObject.FindProperty("RequiredMods");
            itemsProperty = serializedObject.FindProperty("Items");
            iconsProperty = serializedObject.FindProperty("Icons");
            localizationProperty = serializedObject.FindProperty("localization");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            Mod mod = (Mod)target;

            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.TextField("Name", mod.Name);
            EditorGUILayout.TextField("Author", mod.Author);
            EditorGUILayout.PropertyField(localizationProperty, new GUIContent("Localization", "Localization settings for this mod."));
            EditorGUI.EndDisabledGroup();

            mod.Version = EditorGUILayout.TextField(new GUIContent("Version", "The current version of this ModComponent."), mod.Version);
            EditorGUILayout.PropertyField(requiredModsProperty, new GUIContent("Required Mods", "An array of required mods for this ModComponent to work."), true);
            mod.RequiresDLC = EditorGUILayout.Toggle(new GUIContent("Requires DLC", "Set to true if this mod requires TFTFT to work."), mod.RequiresDLC);

            EditorGUILayout.PropertyField(itemsProperty, new GUIContent("Gear Items", "An array of GEAR Items, drag a drop GEAR prefabs into this."), true);
            EditorGUILayout.PropertyField(iconsProperty, new GUIContent("Icons", "An array of Textures, which vary from Inventory Icons to Crafting Icons. DO NOT drag textures for your GEAR Items in here."), true);

            if (GUI.changed)
            {
                EditorUtility.SetDirty(mod);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif