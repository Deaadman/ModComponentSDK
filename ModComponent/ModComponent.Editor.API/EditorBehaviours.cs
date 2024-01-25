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
        protected override Tab[] GetTabs()
        {
            return new[] { Tab.Common, Tab.About };
        }

        protected override void OnEnable()
        {
            base.OnEnable();

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

                { "RepairDurationMinutes", "Repair Duration" },
                { "RecoveryDurationMinutes", "Recovery Duration" },

                { "Condition", "%" },

                { "MinutesMin", "MINS" },
                { "MinutesMax", "MINS" },
                { "ConditionMin", "%" },
                { "ConditionMax", "%" },

                { "UnitsPerItem", "UNITS" },
                { "ChanceFull", "%" },
                { "StackConditionDifferenceConstraint", "%" },
            };
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
            DrawCustomHeading("Default Properties");

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