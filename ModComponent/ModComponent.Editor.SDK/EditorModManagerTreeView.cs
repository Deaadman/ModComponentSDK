#if UNITY_EDITOR
using ModComponent.SDK.Components;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace ModComponent.Editor.SDK
{
    internal class EditorModManagerTreeView : TreeView
    {
        private Dictionary<int, ModDefinition> idToMod;
        private Dictionary<int, string> idToDisplayName;
        private int currentID = 1;

        internal delegate void ItemSelectedAction(ModDefinition modDefinition);
        internal event ItemSelectedAction OnItemSelected;

        internal EditorModManagerTreeView(TreeViewState treeViewState, List<ModDefinition> modDefinitions)
            : base(treeViewState)
        {
            idToMod = new Dictionary<int, ModDefinition>();
            idToDisplayName = new Dictionary<int, string>();
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

                if (modDefinition.Items != null)
                {
                    foreach (var prefab in modDefinition.Items)
                    {
                        int prefabId = currentID++;
                        idToMod.Add(prefabId, modDefinition);
                        idToDisplayName.Add(prefabId, prefab.name);
                    }
                }

                if (modDefinition.Icons != null)
                {
                    foreach (var texture in modDefinition.Icons)
                    {
                        int textureId = currentID++;
                        idToMod.Add(textureId, modDefinition);
                        idToDisplayName.Add(textureId, texture.name);
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
                bool isModItem = kvp.Value.Name == idToDisplayName[kvp.Key];
                int depth = isModItem ? 0 : 1;
                string displayName = idToDisplayName[kvp.Key];
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
                    GameObject selectedPrefab = Array.Find(modDefinition.Items, item => item.name == displayName);
                    if (selectedPrefab != null)
                    {
                        Selection.activeGameObject = selectedPrefab;
                    }
                }
                else if (modDefinition.Icons != null)
                {
                    Texture2D selectedTexture = Array.Find(modDefinition.Icons, item => item.name == displayName);
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
                else if (modDefinition.Items != null && Array.Find(modDefinition.Items, item => item.name == displayName) != null)
                {
                    icon = EditorGUIUtility.IconContent("Prefab Icon").image as Texture2D;
                }
                else if (modDefinition.Icons != null && Array.Find(modDefinition.Icons, item => item.name == displayName) != null)
                {
                    icon = EditorGUIUtility.IconContent("Texture Icon").image as Texture2D;
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
    }
}
#endif