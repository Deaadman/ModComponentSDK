#if UNITY_EDITOR
using ModComponent.Behaviours;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace ModComponent.SDK
{
    public static class PrefabValidator
    {
        internal static List<string> Validate(GameObject prefab, out bool result)
        {
            List<string> messages = new();

            if (prefab == null)
            {
                messages.Add("No prefab selected for validation.");
                result = false;
                return messages;
            }

            System.Type[] incompatibleBehaviors = new System.Type[]
            {
                typeof(ModAccelerantBehaviour),
                typeof(ModBurnableBehaviour),
                typeof(ModFireStarterBehaviour),
                typeof(ModTinderBehaviour),
            };

            int incompatibleBehaviorCount = incompatibleBehaviors.Count(type => prefab.GetComponent(type) != null);
            messages.Add(incompatibleBehaviorCount > 1 ? "Incompatible behaviors detected." : "Incompatible behaviours check passed.");

            Collider[] colliders = prefab.GetComponents<Collider>();
            messages.Add(colliders.Length == 0 ? "No colliders found." : "Colliders check passed.");

            Rigidbody rigidbody = prefab.GetComponent<Rigidbody>();
            messages.Add(rigidbody != null ? "A Rigidbody should not be attached." : "Rigidbody check passed.");

            MeshFilter[] meshFilters = prefab.GetComponents<MeshFilter>();
            MeshRenderer[] meshRenderers = prefab.GetComponents<MeshRenderer>();
            messages.Add((meshFilters.Length > 0 || meshRenderers.Length > 0) ? "The mesh should be a child of the prefab - not the prefab itself." : "Mesh not on prefab check passed.");

            int childrenCount = prefab.transform.childCount;
            messages.Add(childrenCount == 0 ? "This Prefab should have a mesh child GameObject." : "Child mesh GameObject check passed.");

            result = !messages.Any(m => m.Contains("failed"));
            return messages;
        }
    }
}
#endif