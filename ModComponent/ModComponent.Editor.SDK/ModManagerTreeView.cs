#if UNITY_EDITOR
using ModComponent.ModManager;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace ModComponent.Editor.SDK
{
    internal class ModManagerTreeView : TreeView
    {
        private Dictionary<int, Mod> idToMod;
        private Dictionary<int, string> idToDisplayName;
        private int currentID = 1;

        internal delegate void ItemSelectedAction(Mod mod);
        internal event ItemSelectedAction OnItemSelected;

        internal ModManagerTreeView(TreeViewState treeViewState, List<Mod> mods)
            : base(treeViewState)
        {
            idToMod = new Dictionary<int, Mod>();
            idToDisplayName = new Dictionary<int, string>();
            BuildTree(mods);
            Reload();
        }

        private void BuildTree(List<Mod> mods)
        {
            foreach (var mod in mods)
            {
                int modId = currentID++;
                idToMod.Add(modId, mod);
                idToDisplayName.Add(modId, mod.Name);

                if (mod.Items != null)
                {
                    foreach (var prefab in mod.Items)
                    {
                        int prefabId = currentID++;
                        idToMod.Add(prefabId, mod);
                        idToDisplayName.Add(prefabId, prefab.name);
                    }
                }

                if (mod.Icons != null)
                {
                    foreach (var texture in mod.Icons)
                    {
                        int textureId = currentID++;
                        idToMod.Add(textureId, mod);
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
            if (idToMod.TryGetValue(id, out Mod mod))
            {
                string displayName = idToDisplayName[id];

                if (mod.Name == displayName)
                {
                    Selection.activeObject = mod;
                    OnItemSelected?.Invoke(mod);
                }
                else if (mod.Items != null)
                {
                    GameObject selectedPrefab = Array.Find(mod.Items, item => item.name == displayName);
                    if (selectedPrefab != null)
                    {
                        Selection.activeGameObject = selectedPrefab;
                    }
                }
                else if (mod.Icons != null)
                {
                    Texture2D selectedTexture = Array.Find(mod.Icons, item => item.name == displayName);
                    if (selectedTexture != null)
                    {
                        Selection.activeObject = selectedTexture;
                    }
                }
            }
        }

        protected override void RowGUI(RowGUIArgs args)
        {
            if (idToMod.TryGetValue(args.item.id, out Mod mod))
            {
                Rect labelRect = args.rowRect;
                labelRect.x += GetContentIndent(args.item);

                Texture2D icon = null;
                string displayName = idToDisplayName[args.item.id];

                if (mod.Name == displayName)
                {
                    icon = EditorGUIUtility.ObjectContent(mod, typeof(Mod)).image as Texture2D;
                }
                else if (mod.Items != null && Array.Find(mod.Items, item => item.name == displayName) != null)
                {
                    icon = EditorGUIUtility.IconContent("Prefab Icon").image as Texture2D;
                }
                else if (mod.Icons != null && Array.Find(mod.Icons, item => item.name == displayName) != null)
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