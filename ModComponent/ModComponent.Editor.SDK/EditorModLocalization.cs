#if UNITY_EDITOR
using ModComponent.Editor.API;
using ModComponent.SDK.Components;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace ModComponent.Editor.SDK
{
    [CustomEditor(typeof(ModLocalization))]
    internal class ModLocalizationEditor : EditorBase
    {
        private const string LanguageFilterKey = "ModLocalizationEditor_LanguageFilter";

        SerializedProperty localizationEntriesProperty;
        readonly List<string> languages = new()
        {
            "English", "German", "Russian", "French", "Japanese", "Korean", "SimplifiedChinese", "Swedish", "TraditionalChinese",
            "Turkish", "Norwegian", "Spanish", "PortuguesePortugal", "PortugueseBrazil", "Dutch", "Finnish", "Italian",
            "Polish", "Ukrainian"
        };
        int selectedLanguagesMask;

        protected override Tab[] GetTabs() => new[] { Tab.Common };

        protected override void OnEnable()
        {
            base.OnEnable();
            localizationEntriesProperty = serializedObject.FindProperty("localizationEntries");
            selectedLanguagesMask = EditorPrefs.GetInt(LanguageFilterKey, ~0);
        }

        void DrawLanguageFilter()
        {
            GUILayout.BeginVertical(ModComponentEditorStyles.BackgroundBox);
            DrawCustomHeading("Language Filter");

            EditorGUI.BeginChangeCheck();
            selectedLanguagesMask = EditorGUILayout.MaskField("Select Languages", selectedLanguagesMask, languages.ToArray());
            if (EditorGUI.EndChangeCheck())
            {
                EditorPrefs.SetInt(LanguageFilterKey, selectedLanguagesMask);
            }

            GUILayout.EndVertical();
        }

        void DrawLocalizationEntries()
        {
            GUILayout.BeginVertical(ModComponentEditorStyles.BackgroundBox);
            DrawCustomHeading("Localization Entries");

            EditorGUI.BeginChangeCheck();

            for (int i = 0; i < localizationEntriesProperty.arraySize; i++)
            {
                SerializedProperty entryProperty = localizationEntriesProperty.GetArrayElementAtIndex(i);
                GUILayout.BeginVertical("box");
                EditorGUI.indentLevel++;
                entryProperty.isExpanded = EditorGUILayout.Foldout(entryProperty.isExpanded, entryProperty.FindPropertyRelative("localizationKey").stringValue, true);

                if (entryProperty.isExpanded)
                {
                    EditorGUI.indentLevel++;
                    DrawEntry(entryProperty);
                    EditorGUI.indentLevel--;
                }
                EditorGUI.indentLevel--;
                GUILayout.EndVertical();
            }

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Remove Entry", GUILayout.Width(120)) && localizationEntriesProperty.arraySize > 0)
            {
                localizationEntriesProperty.arraySize--;
            }

            if (GUILayout.Button("Add Entry", GUILayout.Width(120)))
            {
                localizationEntriesProperty.arraySize++;
                var newEntry = localizationEntriesProperty.GetArrayElementAtIndex(localizationEntriesProperty.arraySize - 1);
                newEntry.FindPropertyRelative("localizationKey").stringValue = $"Empty Localization Key {localizationEntriesProperty.arraySize - 1}";
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }

            GUILayout.EndVertical();
        }

        void DrawEntry(SerializedProperty entryProperty)
        {
            SerializedProperty keyProperty = entryProperty.FindPropertyRelative("localizationKey");
            EditorGUILayout.PropertyField(keyProperty);

            SerializedProperty languagesProperty = entryProperty.FindPropertyRelative("languages");
            if (languagesProperty.isExpanded = EditorGUILayout.Foldout(languagesProperty.isExpanded, "Languages", true))
            {
                if (selectedLanguagesMask == 0)
                {
                    EditorGUILayout.LabelField("No languages are selected in the filter.", EditorStyles.centeredGreyMiniLabel);
                }
                else
                {
                    EditorGUI.indentLevel++;
                    foreach (var language in languages)
                    {
                        if ((selectedLanguagesMask & (1 << languages.IndexOf(language))) != 0)
                        {
                            SerializedProperty languageProperty = languagesProperty.FindPropertyRelative(language);
                            EditorGUILayout.PropertyField(languageProperty);
                        }
                    }
                    EditorGUI.indentLevel--;
                }
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawLanguageFilter();
            DrawLocalizationEntries();

            if (GUI.changed)
            {
                EditorUtility.SetDirty(target);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif