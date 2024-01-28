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
            return new[] { Tab.Common, Tab.Audio, Tab.About };
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            DefinePropertyUnits();
            DefinePropertyDisplayNames();
            DefineTabDrawMethods();
        }

        private void DefinePropertyUnits()
        {
            propertyUnits = new Dictionary<string, string>()
            {
                { "KeroseneLitersRequired", "L" },
                { "GunpowderKGRequired", "KG" },
                { "DurationMinutes", "MINS" }
            };
        }

        private void DefinePropertyDisplayNames()
        {
            propertyDisplayNames = new Dictionary<string, string>()
            {
                { "Name", "Name (Optional)" },
                { "KeroseneLitersRequired", "Kerosene Required" },
                { "GunpowderKGRequired", "Gunpowder Required" },
                { "DurationMinutes", "Duration Time" }
            };
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

            string[] baseClassProperties = new string[]
            {
                "Name",
                "RequiredGear",
                "RequiredGearUnits",
                "KeroseneLitersRequired",
                "GunpowderKGRequired",
                "RequiredTool",
                "OptionalTools",
                "RequiredCraftingLocation",
                "RequiresLitFire",
                "RequiresLight",
                "CraftedResult",
                "CraftedResultCount",
                "DurationMinutes",
                "AppliedSkill",
                "ImprovedSkill",
            };
            DrawFields(baseClassProperties);

            //if (target.GetType() == typeof(ModRecipe) || target.GetType().IsSubclassOf(typeof(ModRecipe)))
            //{
            //    DrawCustomHeading("Recipe Properties");
            //    DrawFields(new string[] {
            //        "RecipeName",
            //        "RecipeDescription",
            //        "RecipeIcon",
            //        "RequiredSkillLevel",
            //        "AllowedCookingPots"
            //    });
            //}

            GUILayout.EndVertical();
        }

        private void DrawAudioFields()
        {
            GUILayout.BeginVertical(ModComponentEditorStyles.BackgroundBox);
            DrawCustomHeading("Blueprint Audio Properties");

            DrawFields(new string[] {
                "CraftingAudio"
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