#if UNITY_EDITOR
using ModComponent.SDK.Components;
using UnityEditor;
using UnityEngine;

namespace ModComponent.Editor.SDK
{
    [CustomEditor(typeof(DataGearAsset))]
    [CanEditMultipleObjects]
    public class EditorGearAsset : UnityEditor.Editor
    {
        private DataGearAsset Instance => target as DataGearAsset;

        public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
        {
            if (Instance.Icon != null)
            {
                return Instance.Icon;
            }

            return base.RenderStaticPreview(assetPath, subAssets, width, height);
        }
    }
}
#endif