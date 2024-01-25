#if UNITY_EDITOR
using ModComponent.SDK.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace ModComponent.Editor.SDK
{
    internal class EditorModManagerTreeView : TreeView
    {
        private Dictionary<int, ModDefinition> idToMod;
        private Dictionary<int, string> idToDisplayName;
        private Dictionary<int, Texture2D> idToPreview;
        private int currentID = 1;

        internal delegate void ItemSelectedAction(ModDefinition modDefinition);
        internal event ItemSelectedAction OnItemSelected;

        internal EditorModManagerTreeView(TreeViewState treeViewState, List<ModDefinition> modDefinitions)
            : base(treeViewState)
        {
            idToMod = new Dictionary<int, ModDefinition>();
            idToDisplayName = new Dictionary<int, string>();
            idToPreview = new Dictionary<int, Texture2D>();
            BuildTree(modDefinitions);
            Reload();
        }

        private void BuildTree(List<ModDefinition> modDefinitions)
        {
            foreach (var modDefinition in modDefinitions)
            {
                int modId = currentID++;
                idToMod.Add(modId, modDefinition);
                idToDisplayName.Add(modId, modDefinition.Name);
                idToPreview.Add(modId, AssetPreview.GetAssetPreview(modDefinition));

                int prefabsId = currentID++;
                idToMod.Add(prefabsId, modDefinition);
                idToDisplayName.Add(prefabsId, "Prefabs");

                if (modDefinition.Items != null)
                {
                    foreach (var prefab in modDefinition.Items)
                    {
                        int prefabId = currentID++;
                        idToMod.Add(prefabId, modDefinition);
                        idToDisplayName.Add(prefabId, prefab.name);
                        idToPreview.Add(prefabId, AssetPreview.GetAssetPreview(prefab));
                    }
                }

                int texturesId = currentID++;
                idToMod.Add(texturesId, modDefinition);
                idToDisplayName.Add(texturesId, "Textures");

                if (modDefinition.Icons != null)
                {
                    foreach (var texture in modDefinition.Icons)
                    {
                        int textureId = currentID++;
                        idToMod.Add(textureId, modDefinition);
                        idToDisplayName.Add(textureId, texture.name);
                        idToPreview.Add(textureId, texture);
                    }
                }
            }
        }

        protected override TreeViewItem BuildRoot()
        {
            var root = new TreeViewItem { id = 0, depth = -1, displayName = "Root" };
            var allItems = new List<TreeViewItem>();

            foreach (var kvp in idToMod)
            {
                string displayName = idToDisplayName[kvp.Key];
                bool isModItem = kvp.Value.Name == displayName;
                bool isPrefabOrTextureGroup = displayName == "Prefabs" || displayName == "Textures";
                int depth = isModItem ? 0 : isPrefabOrTextureGroup ? 1 : 2;
                allItems.Add(new TreeViewItem { id = kvp.Key, depth = depth, displayName = displayName });
            }

            SetupParentsAndChildrenFromDepths(root, allItems);

            return root;
        }

        protected override void SingleClickedItem(int id)
        {
            base.SingleClickedItem(id);
            if (idToMod.TryGetValue(id, out ModDefinition modDefinition))
            {
                string displayName = idToDisplayName[id];

                if (modDefinition.Name == displayName)
                {
                    Selection.activeObject = modDefinition;
                    OnItemSelected?.Invoke(modDefinition);
                }
                else if (modDefinition.Items != null)
                {
                    GameObject selectedPrefab = modDefinition.Items.FirstOrDefault(item => item.name == displayName);
                    if (selectedPrefab != null)
                    {
                        Selection.activeGameObject = selectedPrefab;
                    }
                }
                else if (modDefinition.Icons != null)
                {
                    Texture2D selectedTexture = modDefinition.Icons.FirstOrDefault(item => item.name == displayName);
                    if (selectedTexture != null)
                    {
                        Selection.activeObject = selectedTexture;
                    }
                }
            }
        }

        protected override void RowGUI(RowGUIArgs args)
        {
            if (idToMod.TryGetValue(args.item.id, out ModDefinition modDefinition))
            {
                Rect labelRect = args.rowRect;
                labelRect.x += GetContentIndent(args.item);

                Texture2D icon = null;
                string displayName = idToDisplayName[args.item.id];

                if (modDefinition.Name == displayName)
                {
                    icon = EditorGUIUtility.ObjectContent(modDefinition, typeof(ModDefinition)).image as Texture2D;
                }
                else if (displayName == "Prefabs")
                {
                    icon = EditorGUIUtility.IconContent("Prefab Icon").image as Texture2D;
                }
                else if (displayName == "Textures")
                {
                    icon = EditorGUIUtility.IconContent("Texture Icon").image as Texture2D;
                }
                else if (idToPreview.TryGetValue(args.item.id, out Texture2D preview))
                {
                    icon = preview;
                }

                if (icon != null)
                {
                    Rect iconRect = labelRect;
                    iconRect.width = iconRect.height;
                    GUI.DrawTexture(iconRect, icon);
                    labelRect.x += iconRect.width + 4;
                }

                EditorGUI.LabelField(labelRect, displayName);
            }
        }

        public new void OnGUI(Rect rect)
        {
            base.OnGUI(rect);

            float labelAreaHeight = 20;
            float labelAreaPaddingLeft = 10;
            Rect labelAreaRect = new(rect.x + labelAreaPaddingLeft, rect.yMax - labelAreaHeight, rect.width - labelAreaPaddingLeft, labelAreaHeight);

            GUILayout.BeginArea(labelAreaRect);
            GUILayout.BeginVertical();
            //GUILayout.Label("Mod Manager Toolbox", EditorStyles.boldLabel);
            GUILayout.EndVertical();
            GUILayout.EndArea();

            GUILayout.BeginArea(new Rect(rect.x, rect.yMax, rect.width, 30));
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Expand All", GUILayout.Width(120)))
            {
                ExpandAll();
            }

            if (GUILayout.Button("Collapse All", GUILayout.Width(120)))
            {
                CollapseAll();
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
    }
}
#endif