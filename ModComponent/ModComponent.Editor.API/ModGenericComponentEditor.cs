#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
using ModComponent.Components;

namespace ModComponent.Editor.API
{
    [CustomEditor(typeof(ModGenericComponent))]
    public class ModGenericComponentEditor : UnityEditor.Editor
    {
        enum Tab { Common, Audio, Inspect, About }
        private Tab selectedTab = Tab.Common;

        private readonly Dictionary<string, SerializedProperty> serializedProperties = new();
        private Dictionary<Tab, System.Action> tabDrawMethods;
        private readonly Dictionary<string, string> propertyUnits = new()
        {
            { "WeightKG", "KG" }
        };
        private readonly Dictionary<string, string> propertyDisplayNames = new()
        {
            { "DisplayNameLocalizationId", "Display Name Localization Key"},
            { "DescriptionLocalizatonId", "Description Localization Key"},
            { "InventoryActionLocalizationId", "Inventory Action Localization Key"},
            { "WeightKG", "Weight" },
            { "MaxHP", "Max Health" },
        };

        private void OnEnable()
        {
            foreach (FieldInfo field in typeof(ModGenericComponent).GetFields(BindingFlags.Public | BindingFlags.Instance))
            {
                SerializedProperty property = serializedObject.FindProperty(field.Name);
                if (property != null)
                {
                    serializedProperties[field.Name] = property;
                }
            }

            tabDrawMethods = new Dictionary<Tab, System.Action>
            {
                { Tab.Common, () => DrawFields(new string[] {
                    "DisplayNameLocalizationId",
                    "DescriptionLocalizatonId",
                    "InventoryActionLocalizationId",
                    "WeightKG",
                    "DaysToDecay",
                    "MaxHP",
                    "initialCondition",
                    "inventoryCategory"
                })},

                { Tab.Audio, () => DrawFields(new string[] {
                    "PickUpAudio", 
                    "PutBackAudio", 
                    "StowAudio", 
                    "WornOutAudio"
                })},

                { Tab.Inspect, () => DrawFields(new string[] {
                    "InspectOnPickup", 
                    "InspectDistance", 
                    "InspectAngles", 
                    "InspectOffset", 
                    "InspectScale", 
                    "NormalModel", 
                    "InspectModel"
                })},

                { Tab.About, () => DrawAboutFields() },
            };
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            GUILayout.BeginHorizontal();
            foreach (Tab tab in System.Enum.GetValues(typeof(Tab)))
            {
                DrawTabButton(tab, tab.ToString());
            }
            GUILayout.EndHorizontal();

            tabDrawMethods[selectedTab]();

            if (GUI.changed)
            {
                EditorUtility.SetDirty(target);
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawTabButton(Tab tab, string label)
        {
            bool isSelected = selectedTab == tab;
            GUI.backgroundColor = isSelected ? Color.gray : Color.white;
            if (GUILayout.Button(label))
            {
                selectedTab = tab;
            }
            GUI.backgroundColor = Color.white;
        }

        private void DrawFields(string[] propertyNames)
        {
            foreach (string name in propertyNames)
            {
                if (serializedProperties.TryGetValue(name, out SerializedProperty property))
                {
                    string displayName = propertyDisplayNames.ContainsKey(name) ? propertyDisplayNames[name] : ObjectNames.NicifyVariableName(name);
                    if (propertyUnits.TryGetValue(name, out string unit))
                    {
                        ModComponentEditorStyles.DrawPropertyWithUnit(property, unit, new GUIContent(displayName));
                    }
                    else
                    {
                        EditorGUILayout.PropertyField(property, new GUIContent(displayName));
                    }
                }
            }
        }

        private void DrawAboutFields()
        {
            EditorGUILayout.BeginScrollView(Vector2.zero);
            EditorContentDrawer.DrawWelcomeContent();
            EditorGUILayout.EndScrollView();
        }
    }
}
#endif