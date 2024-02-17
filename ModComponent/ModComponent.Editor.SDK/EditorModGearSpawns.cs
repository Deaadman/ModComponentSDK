#if UNITY_EDITOR
using ModComponent.Editor.API;
using ModComponent.SDK.Components;
using UnityEditor;
using UnityEngine;

namespace ModComponent.Editor.SDK
{
    [CustomEditor(typeof(ModGearSpawns))]
    internal class EditorModGearSpawns : EditorBase
    {
        private SerializedProperty sceneGearSpawnEntriesProperty;
        private SerializedProperty lootTableGearSpawnEntriesProperty;

        protected override Tab[] GetTabs() => new[] { Tab.Common };

        protected override void OnEnable()
        {
            sceneGearSpawnEntriesProperty = serializedObject.FindProperty("sceneGearSpawnEntries");
            lootTableGearSpawnEntriesProperty = serializedObject.FindProperty("lootTableGearSpawnEntries");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            GUILayout.BeginVertical(ModComponentEditorStyles.BackgroundBox);
            DrawCustomHeading("Gear Spawn Entries");

            EditorGUILayout.PropertyField(sceneGearSpawnEntriesProperty, new GUIContent("Scene Spawn Entries", "All following item spawn definitions will use that scene, until another scene is defined. E.g. FarmhouseA."), true);
            EditorGUILayout.PropertyField(lootTableGearSpawnEntriesProperty, new GUIContent("Loot Table Spawn Entries", "All following loot table entry definitions will use that loot table, until another loot table is defined. E.g. LootTableVehicleGloveBox."), true);

            GUILayout.EndVertical();
            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif