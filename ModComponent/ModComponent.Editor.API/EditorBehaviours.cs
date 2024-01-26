#if UNITY_EDITOR
using ModComponent.Behaviours;
using ModComponent.Editor.SDK;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace ModComponent.Editor.API
{
    [CustomEditor(typeof(ModBaseBehaviour), true)]
    internal class EditorBehaviours : EditorBase
    {
        private readonly HashSet<string> audioPropertyNames = new()
        {
            "Audio",
            "OnUseSoundEvent"
        };

        protected override Tab[] GetTabs()
        {
            var tabs = new List<Tab> { Tab.Common };

            if (HasAudioProperties())
            {
                tabs.Add(Tab.Audio);
            }

            tabs.Add(Tab.About);

            return tabs.ToArray();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            DefinePropertyUnits();
            DefinePropertyDisplayNames();

            CacheSerializedProperties();
            DefineTabDrawMethods();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawTabButtons();
            DrawSelectedTab();

            if (GUI.changed)
            {
                EditorUtility.SetDirty(target);
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void DefinePropertyUnits()
        {
            propertyUnits = new Dictionary<string, string>()
            {
                { "DurationOffset", "SECS" },
                { "SuccessModifier", "%" },

                { "BurningMinutes", "MINS" },
                { "BurningMinutesBeforeAllowedToAdd", "MINS" },
                { "TempIncrease", "°C" },

                { "MaxCarryCapacityKGBuff", "KG" },

                { "EvolveHours", "HRS" },

                { "SecondsToIgniteTinder", "SECS" },
                { "SecondsToIgniteTorch", "SECS" },

                { "Minutes", "MINS" },

                { "RepairDurationMinutes", "MINS" },
                { "RecoveryDurationMinutes", "MINS" },

                { "Condition", "%" },

                { "MinutesMin", "MINS" },
                { "MinutesMax", "MINS" },
                { "ConditionMin", "%" },
                { "ConditionMax", "%" },

                { "UnitsPerItem", "UNITS" },
                { "ChanceFull", "%" },
                { "StackConditionDifferenceConstraint", "%" },
            };
        }

        private void DefinePropertyDisplayNames()
        {
            propertyDisplayNames = new Dictionary<string, string>()
            {
                { "BurningMinutes", "Burning" },
                { "BurningMinutesBeforeAllowedToAdd", "Burning Before Allowed To Add" },
                { "TempIncrease", "Temperature Increase" },

                { "MaxCarryCapacityKGBuff", "Maximum Carry Capacity Buff" },

                { "EvolveHours", "Evolve Duration" },

                { "SecondsToIgniteTinder", "Ignite Tinder" },
                { "SecondsToIgniteTorch", "Ignite Torch" },

                { "Minutes", "Duration" },

                { "RepairDurationMinutes", "Repair Duration" },
                { "RecoveryDurationMinutes", "Recovery Duration" },

                { "MinutesMin", "Minutes Minimum" },
                { "MinutesMax", "Minutes Maximum" },
                { "ConditionMin", "Condition Minimum" },
                { "ConditionMax", "Condition Maximum" },

                { "SingleUnitTextId", "Single Unit Localization Key" },
                { "MultipleUnitTextId", "Multiple Unit Localization Key" },
                { "UnitsPerItem", "Full Stack" },
                { "StackConditionDifferenceConstraint", "Stack Condition Difference" },
            };
        }

        private void CacheSerializedProperties()
        {
            foreach (FieldInfo field in target.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
            {
                SerializedProperty property = serializedObject.FindProperty(field.Name);
                if (property != null)
                {
                    serializedProperties[field.Name] = property;
                }
            }
        }

        private void DefineTabDrawMethods()
        {
            tabDrawMethods = new Dictionary<Tab, Action>
            {
                { Tab.Common, () => DrawCommonFields() },
                { Tab.Audio, () => DrawAudioFields() },
                { Tab.About, () => DrawAboutFields() }
            };
        }

        private void DrawTabButtons()
        {
            GUILayout.BeginHorizontal();
            foreach (Tab tab in GetTabs())
            {
                DrawTabButton(tab, tab.ToString());
            }
            GUILayout.EndHorizontal();
        }

        private void DrawSelectedTab()
        {
            if (tabDrawMethods.TryGetValue(selectedTab, out Action drawMethod))
            {
                drawMethod();
            }
        }

        private void DrawCommonFields()
        {
            GUILayout.BeginVertical(ModComponentEditorStyles.BackgroundBox);
            DrawCustomHeading("Behaviour Properties");

            DrawProperties(excludeAudio: true);

            GUILayout.EndVertical();
        }

        private void DrawAudioFields()
        {
            if (!HasAudioProperties())
            {
                return;
            }

            GUILayout.BeginVertical(ModComponentEditorStyles.BackgroundBox);
            DrawCustomHeading("Behaviour Audio Properties");

            foreach (string audioPropertyName in audioPropertyNames)
            {
                DrawAudioProperty(audioPropertyName);
            }

            GUILayout.EndVertical();
        }

        private void DrawAudioProperty(string propertyName)
        {
            if (serializedProperties.TryGetValue(propertyName, out SerializedProperty property))
            {
                string displayName = propertyDisplayNames.ContainsKey(propertyName)
                                     ? propertyDisplayNames[propertyName]
                                     : ObjectNames.NicifyVariableName(propertyName);

                if (propertyUnits.TryGetValue(propertyName, out string unit))
                {
                    ModComponentEditorStyles.DrawPropertyWithUnit(property, unit, new GUIContent(displayName));
                }
                else
                {
                    EditorGUILayout.PropertyField(property, new GUIContent(displayName));
                }
            }
        }

        private void DrawProperties(bool excludeAudio = false)
        {
            foreach (var pair in serializedProperties)
            {
                if (excludeAudio && audioPropertyNames.Contains(pair.Key))
                {
                    continue;
                }

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
        }

        private bool HasAudioProperties()
        {
            foreach (var audioPropertyName in audioPropertyNames)
            {
                if (serializedProperties.ContainsKey(audioPropertyName))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
#endif