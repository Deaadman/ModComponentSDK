using UnityEditor;
using UnityEngine;

namespace ModComponent.Common
{
    public static class ModComponentGUIStyles
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
    }
}