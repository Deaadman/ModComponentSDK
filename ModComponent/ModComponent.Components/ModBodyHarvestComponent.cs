using ModComponent.SDK.Components;
using UnityEngine;

namespace ModComponent.Components
{
    [HelpURL("https://github.com/dommrogers/ModComponent/blob/master/docs/Body-Harvest-Component-Documentation.md")]
    public class ModBodyHarvestComponent : ModGenericComponent
    {
        [Tooltip("Can this be carried like a rabbit carcass?")]
        public bool CanCarry;

        [Tooltip("The id for the sound to be played while harvesting.")]
        public string HarvestAudio;

        [Tooltip("The name of the object prefab for the guts.")]
        public GearAsset GutPrefab;

        [Tooltip("The number of guts in each harvest.")]
        public int GutQuantity;

        [Tooltip("The weight of the gut before the player harvests it.")]
        public float GutWeightKgPerUnit;

        [Tooltip("The name of the object prefab for the hide.")]
        public GearAsset HidePrefab;

        [Tooltip("The number of hides in each harvest.")]
        public int HideQuantity;

        [Tooltip("The weight of the hide before the player harvests it.")]
        public float HideWeightKgPerUnit;

        [Tooltip("The name of the object prefab for the raw meat.")]
        public GearAsset MeatPrefab;

        [Tooltip("The minimum amount of meat in each harvest.")]
        public float MeatAvailableMinKG;

        [Tooltip("The maximum amount of meat in each harvest.")]
        public float MeatAvailableMaxKG;
    }
}