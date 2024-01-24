#if UNITY_EDITOR
using ModComponent.Components;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ModComponent.Editor.API
{
    [CustomEditor(typeof(ModGenericComponent), true)]
    internal class ComponentEditor : ModEditorBase
    {
        protected override Tab[] GetTabs()
        {
            return new[] { Tab.Common, Tab.Audio, Tab.Inspect, Tab.About };
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            propertyUnits = new Dictionary<string, string>()
            {
                { "WeightKG", "KG" },
                { "DaysToDecay", "DAYS" },
                { "MaxHP", "HP" },
                { "ConditionGainPerHour", "% / HR" },
                { "AdditionalConditionGainPerHour", "% / HR" },
                { "WarmthBonusCelsius", "°C" },
                { "DegradePerHour", "% / HR" }
            };

            propertyDisplayNames = new Dictionary<string, string>()
            {
                { "DisplayNameLocalizationId", "Display Name Localization Key"},
                { "DescriptionLocalizatonId", "Description Localization Key"},
                { "InventoryActionLocalizationId", "Inventory Action Localization Key"},
                { "WeightKG", "Weight" },
                { "DaysToDecay", "Decay" },
                { "MaxHP", "Max Health" },
                { "ConditionGainPerHour", "Condition Gain" },
                { "AdditionalConditionGainPerHour", "Additional Condition Gain" },
                { "WarmthBonusCelsius", "Warmth Bonus" },
                { "DegradePerHour", "Degrade" }
            };

            tabDrawMethods = new Dictionary<Tab, Action>
            {
                { Tab.Common, () => DrawCommonFields() },
                { Tab.Audio, () => DrawAudioFields() },
                { Tab.Inspect, () => DrawInspectFields() },
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

            string[] baseClassProperties = new string[]
            {
                "DisplayNameLocalizationId",
                "DescriptionLocalizatonId",
                "InventoryActionLocalizationId",
                "WeightKG",
                "DaysToDecay",
                "MaxHP",
                "InitialCondition",
                "InventoryCategory"
            };
            DrawFields(baseClassProperties);

            if (target.GetType() == typeof(ModAmmoComponent) || target.GetType().IsSubclassOf(typeof(ModAmmoComponent)))
            {
                DrawCustomHeading("Ammo Component Properties");
                DrawFields(new string[] { "AmmoForGunType" });
            }

            if (target.GetType() == typeof(ModBedComponent) || target.GetType().IsSubclassOf(typeof(ModBedComponent)))
            {
                DrawCustomHeading("Bed Component Properties");
                DrawFields(new string[] {
                    "ConditionGainPerHour",
                    "AdditionalConditionGainPerHour",
                    "WarmthBonusCelsius",
                    "DegradePerHour",
                    "BearAttackModifier",
                    "WolfAttackModifier"
                });
            }

            GUILayout.EndVertical();
        }

        private void DrawAudioFields()
        {
            GUILayout.BeginVertical(ModComponentEditorStyles.BackgroundBox);
            DrawCustomHeading("Default Audio Properties");

            string[] baseClassProperties = new string[]
            {
                "PickUpAudio",
                "PutBackAudio",
                "StowAudio",
                "WornOutAudio"
            };
            DrawFields(baseClassProperties);

            if (target.GetType() == typeof(ModBedComponent) || target.GetType().IsSubclassOf(typeof(ModBedComponent)))
            {
                DrawCustomHeading("Bed Component Audio Properties");
                DrawFields(new string[] {
                    "OpenAudio",
                    "CloseAudio",
                    "PackedMesh",
                    "UsableMesh"
                });
            }

            GUILayout.EndVertical();
        }

        private void DrawInspectFields()
        {
            GUILayout.BeginVertical(ModComponentEditorStyles.BackgroundBox);
            DrawCustomHeading("Default Inspect Properties");

            DrawFields(new string[] {
                "InspectOnPickup",
                "InspectDistance",
                "InspectAngles",
                "InspectOffset",
                "InspectScale",
                "NormalModel",
                "InspectModel"
            });

            GUILayout.EndVertical();
        }

        private void DrawFields(string[] propertyNames)
        {
            GUILayout.BeginVertical(ModComponentEditorStyles.BackgroundBox);
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
            GUILayout.EndVertical();
        }
    }
}
#endif