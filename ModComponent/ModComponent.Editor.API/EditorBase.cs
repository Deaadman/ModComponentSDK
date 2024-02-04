#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ModComponent.Editor.API
{
    internal abstract class EditorBase : UnityEditor.Editor
    {
        protected enum Tab { Common, Audio, Inspect, About }
        protected Tab selectedTab = Tab.Common;

        protected Dictionary<string, SerializedProperty> serializedProperties;
        protected Dictionary<Tab, Action> tabDrawMethods;
        protected Dictionary<string, string> propertyUnits;
        protected Dictionary<string, string> propertyDisplayNames;

        protected void SetSelectedTab(Tab tab)
        {
            selectedTab = tab;
        }

        protected virtual void OnEnable()
        {
            serializedProperties = new Dictionary<string, SerializedProperty>();

            foreach (var field in target.GetType().GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
            {
                SerializedProperty property = serializedObject.FindProperty(field.Name);
                if (property != null)
                {
                    serializedProperties[field.Name] = property;
                }
            }
        }

        protected abstract Tab[] GetTabs();

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

        protected void DrawAboutFields()
        {
            EditorGUILayout.BeginScrollView(Vector2.zero);
            EditorHub.DrawMainContent();
            EditorGUILayout.EndScrollView();
        }

        protected void DrawArrayProperty(SerializedProperty arrayProperty, string label)
        {
            if (arrayProperty != null && arrayProperty.isArray)
            {
                GUILayout.BeginVertical("box");
                EditorGUI.indentLevel++;
                arrayProperty.isExpanded = EditorGUILayout.Foldout(arrayProperty.isExpanded, label, true);

                if (arrayProperty.isExpanded)
                {
                    EditorGUI.indentLevel++;
                    for (int i = 0; i < arrayProperty.arraySize; i++)
                    {
                        EditorGUILayout.PropertyField(arrayProperty.GetArrayElementAtIndex(i));
                    }
                    EditorGUI.indentLevel--;

                    GUILayout.BeginHorizontal();
                    GUILayout.FlexibleSpace();
                    if (GUILayout.Button("Add Element", GUILayout.Width(120)))
                    {
                        arrayProperty.arraySize++;
                    }
                    if (GUILayout.Button("Remove Last Element", GUILayout.Width(150)) && arrayProperty.arraySize > 0)
                    {
                        arrayProperty.arraySize--;
                    }
                    GUILayout.FlexibleSpace();
                    GUILayout.EndHorizontal();
                }
                EditorGUI.indentLevel--;
                GUILayout.EndVertical();
            }
        }

        protected bool DrawTabButton(Tab tab, string label)
        {
            bool isSelected = selectedTab == tab;
            GUI.backgroundColor = isSelected ? Color.gray : Color.white;
            if (GUILayout.Button(label))
            {
                selectedTab = tab;
                GUI.backgroundColor = Color.white;
                return true;
            }
            GUI.backgroundColor = Color.white;
            return false;
        }

        protected void DrawCustomHeading(string headingText)
        {
            var style = new GUIStyle(GUI.skin.label)
            {
                fontStyle = FontStyle.Bold,
                normal = { textColor = Color.white, background = MakeTex(2, 2, new Color(0.2f, 0.2f, 0.2f, 1f)) },
                padding = new RectOffset(6, 6, 4, 4),
                fontSize = 11,
                alignment = TextAnchor.MiddleLeft
            };
            GUILayout.Label(headingText, style);
        }

        protected Texture2D MakeTex(int width, int height, Color col)
        {
            Color32[] pix = new Color32[width * height];
            for (int i = 0; i < pix.Length; i++)
                pix[i] = col;
            Texture2D result = new(width, height);
            result.SetPixels32(pix);
            result.Apply();
            return result;
        }
    }
}
#endif