#if UNITY_EDITOR
using ModComponent.Editor.API;
using ModComponent.SDK.Components;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace ModComponent.Editor.SDK
{
    [CustomEditor(typeof(ModGearSpawns))]
    internal class EditorModGearSpawns : EditorBase
    {
        SerializedProperty sceneGearSpawnEntriesProperty;
        SerializedProperty lootTableGearSpawnEntriesProperty;

        protected override Tab[] GetTabs() => new[] { Tab.Common };

        protected override void OnEnable()
        {
            base.OnEnable();
            sceneGearSpawnEntriesProperty = serializedObject.FindProperty("sceneGearSpawnEntries");
            lootTableGearSpawnEntriesProperty = serializedObject.FindProperty("lootTableGearSpawnEntries");
        }

        void DrawButtonCentered(string buttonText, System.Action action)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button(buttonText, GUILayout.Width(160)))
            {
                action?.Invoke();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        void DrawSceneGearSpawnEntries()
        {
            GUILayout.BeginVertical(ModComponentEditorStyles.BackgroundBox);
            DrawCustomHeading("Scene Gear Spawn Entries");

            for (int i = 0; i < sceneGearSpawnEntriesProperty.arraySize; i++)
            {
                SerializedProperty entry = sceneGearSpawnEntriesProperty.GetArrayElementAtIndex(i);
                SerializedProperty sceneName = entry.FindPropertyRelative("sceneName");
                SerializedProperty sceneItemSpawns = entry.FindPropertyRelative("sceneItemSpawns");

                GUILayout.BeginVertical("box");
                EditorGUI.indentLevel++;
                sceneName.isExpanded = EditorGUILayout.Foldout(sceneName.isExpanded, sceneName.objectReferenceValue != null ? sceneName.objectReferenceValue.name : "New Scene Spawn Entry", true);

                if (sceneName.isExpanded)
                {
                    EditorGUILayout.PropertyField(sceneName);
                    DrawSceneItemSpawns(sceneItemSpawns);
                }

                EditorGUI.indentLevel--;
                GUILayout.EndVertical();

                DrawButtonCentered("Remove Entry", () => {
                    sceneGearSpawnEntriesProperty.DeleteArrayElementAtIndex(i);
                    serializedObject.ApplyModifiedProperties();
                });
            }

            DrawButtonCentered("Add Entry", () => {
                sceneGearSpawnEntriesProperty.arraySize++;
                serializedObject.ApplyModifiedProperties();
            });

            GUILayout.EndVertical();
        }

        void DrawSceneItemSpawns(SerializedProperty sceneItemSpawns)
        {
            EditorGUI.indentLevel++;
            for (int i = 0; i < sceneItemSpawns.arraySize; i++)
            {
                SerializedProperty spawn = sceneItemSpawns.GetArrayElementAtIndex(i);
                EditorGUILayout.PropertyField(spawn);

                DrawButtonCentered($"Remove Item Spawn {i + 1}", () => {
                    sceneItemSpawns.DeleteArrayElementAtIndex(i);
                    serializedObject.ApplyModifiedProperties();
                });
            }
            EditorGUI.indentLevel--;

            DrawButtonCentered("Add Item Spawn", () => {
                sceneItemSpawns.arraySize++;
                serializedObject.ApplyModifiedProperties();
            });
        }

        void DrawLootTableGearSpawnEntries()
        {
            GUILayout.BeginVertical(ModComponentEditorStyles.BackgroundBox);
            DrawCustomHeading("Loot Table Gear Spawn Entries");

            for (int i = 0; i < lootTableGearSpawnEntriesProperty.arraySize; i++)
            {
                SerializedProperty entry = lootTableGearSpawnEntriesProperty.GetArrayElementAtIndex(i);
                SerializedProperty lootTableName = entry.FindPropertyRelative("lootTableName");
                SerializedProperty lootTableSpawns = entry.FindPropertyRelative("lootTableSpawns");

                GUILayout.BeginVertical("box");
                EditorGUI.indentLevel++;
                lootTableName.isExpanded = EditorGUILayout.Foldout(lootTableName.isExpanded, lootTableName.objectReferenceValue != null ? lootTableName.objectReferenceValue.name : "New Loot Table Spawn Entry", true);

                if (lootTableName.isExpanded)
                {
                    EditorGUILayout.PropertyField(lootTableName);
                    DrawLootTableSpawns(lootTableSpawns);
                }

                EditorGUI.indentLevel--;
                GUILayout.EndVertical();

                DrawButtonCentered("Remove Entry", () => {
                    lootTableGearSpawnEntriesProperty.DeleteArrayElementAtIndex(i);
                    serializedObject.ApplyModifiedProperties();
                });
            }

            DrawButtonCentered("Add Entry", () => {
                lootTableGearSpawnEntriesProperty.arraySize++;
                serializedObject.ApplyModifiedProperties();
            });

            GUILayout.EndVertical();
        }

        void DrawLootTableSpawns(SerializedProperty lootTableSpawns)
        {
            EditorGUI.indentLevel++;
            for (int i = 0; i < lootTableSpawns.arraySize; i++)
            {
                SerializedProperty spawn = lootTableSpawns.GetArrayElementAtIndex(i);
                EditorGUILayout.PropertyField(spawn);

                DrawButtonCentered($"Remove Loot Spawn {i + 1}", () => {
                    lootTableSpawns.DeleteArrayElementAtIndex(i);
                    serializedObject.ApplyModifiedProperties();
                });
            }
            EditorGUI.indentLevel--;

            DrawButtonCentered("Add Loot Spawn", () => {
                lootTableSpawns.arraySize++;
                serializedObject.ApplyModifiedProperties();
            });
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawSceneGearSpawnEntries();
            DrawLootTableGearSpawnEntries();

            if (GUI.changed)
            {
                EditorUtility.SetDirty(target);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif
