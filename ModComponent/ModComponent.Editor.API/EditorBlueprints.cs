#if UNITY_EDITOR
using ModComponent.Components;
using ModComponent.Editor.SDK;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace ModComponent.Editor.API
{
    [CustomEditor(typeof(ModBlueprint), true)]
    internal class EditorBlueprints : EditorBase
    {
        protected override Tab[] GetTabs()
        {
            return new[] { Tab.Common, Tab.About };
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            propertyUnits = new Dictionary<string, string>()
            {
                { "KeroseneLitersRequired", "L" },
                { "GunpowderKGRequired", "KG" },
                { "DurationMinutes", "MINS" }
            };
            propertyDisplayNames = new Dictionary<string, string>()
            {
                { "Name", "Name (Optional)" },
                { "KeroseneLitersRequired", "Kerosene Required" },
                { "GunpowderKGRequired", "Gunpowder Required" },
                { "DurationMinutes", "Duration Time" }
            };

            foreach (FieldInfo field in target.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
            {
                SerializedProperty property = serializedObject.FindProperty(field.Name);
                if (property != null)
                {
                    serializedProperties[field.Name] = property;
                }
            }

            tabDrawMethods = new Dictionary<Tab, Action>
            {
                { Tab.Common, () => DrawCommonFields() },
                { Tab.About, () => DrawAboutFields() }
            };
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            GUILayout.BeginHorizontal();
            foreach (Tab tab in GetTabs())
            {
                DrawTabButton(tab, tab.ToString());
            }
            GUILayout.EndHorizontal();

            if (tabDrawMethods.TryGetValue(selectedTab, out Action drawMethod))
            {
                drawMethod();
            }

            if (GUI.changed)
            {
                EditorUtility.SetDirty(target);
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawCommonFields()
        {
            GUILayout.BeginVertical(ModComponentEditorStyles.BackgroundBox);
            DrawCustomHeading("Blueprint Properties");

            foreach (var pair in serializedProperties)
            {
                SerializedProperty property = pair.Value;
                string displayName = propertyDisplayNames.ContainsKey(pair.Key)
                                     ? propertyDisplayNames[pair.Key]
                                     : ObjectNames.NicifyVariableName(pair.Key);

                if (propertyUnits.TryGetValue(pair.Key, out string unit))
                {
                    ModComponentEditorStyles.DrawPropertyWithUnit(property, unit, new GUIContent(displayName));
                }
                else
                {
                    EditorGUILayout.PropertyField(property, new GUIContent(displayName));
                }
            }

            GUILayout.EndVertical();
        }
    }
}
#endif