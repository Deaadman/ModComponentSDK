using UnityEditor;
using UnityEngine;

namespace ModComponent.Editor
{
    public static class ModComponentEditorStyles
    {
        public static GUIStyle BackgroundBox = new(EditorStyles.helpBox)
        {
            margin = new RectOffset(10, 10, 10, 12),
            padding = new RectOffset(10, 10, 10, 12)
        };

        public static GUIStyle CenteredLabelBold = new(EditorStyles.label)
        {
            alignment = TextAnchor.MiddleCenter,
            fontStyle = FontStyle.Bold,
            wordWrap = true
        };

        public static GUIStyle CenteredLabel = new(EditorStyles.label)
        {
            alignment = TextAnchor.MiddleCenter,
            wordWrap = true
        };

        public static GUIStyle CenteredGreyMiniLabel = new(EditorStyles.centeredGreyMiniLabel)
        {
            wordWrap = true
        };

        public static GUIStyle WrappedLabel = new(EditorStyles.label)
        {
            wordWrap = true,
            fontSize = 11
        };

        public static void DrawPropertyWithUnit(SerializedProperty property, string unit, GUIContent label = null)
        {
            Rect position = EditorGUILayout.GetControlRect();
            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.PropertyField(position, property, label ?? GUIContent.none);

            var unitStyle = new GUIStyle(EditorStyles.label)
            {
                alignment = TextAnchor.MiddleRight,
                fontSize = 10
            };
            var unitRect = new Rect(position.xMax - 40, position.y, 35, position.height);

            GUI.Label(unitRect, unit, unitStyle);
            EditorGUI.EndProperty();
        }
    }
}