using ModComponent.SDK.Components;
using ModComponent.Utilities;
using UnityEngine;

namespace ModComponent.Components
{
    [HelpURL("https://github.com/dommrogers/ModComponent/blob/master/docs/Cookable-Component-Documentation.md")]
    public class ModCookableComponent : ModGenericComponent
    {
        [Tooltip("Can this be cooked/heated? If not enabled, the other settings in this section will be ignored.")]
        public bool Cooking = true;

        [Tooltip("How many in-game minutes does it take to cook/heat this item?")]
        public int CookingMinutes;

        [Tooltip("How many units of this item are required for cooking?")]
        public int CookingUnitsRequired;

        [Tooltip("How many liters of water are required for cooking this item? Only potable water applies.")]
        public float CookingWaterRequired;

        [Tooltip("Convert the item into this item when cooking completes. Leave empty to only heat the item without converting it.")]
        public GearAsset CookingResult;

        [Tooltip("How many in-game minutes until this item becomes burnt after being 'cooked'?")]
        public int BurntMinutes;

        [Tooltip("What type of cookable is this? Affects where and how this item can be cooked.")]
        public CookableType Type;

        [Tooltip("Sound to use when cooking/heating the item. Leave empty for a sensible default.")]
        public string CookingAudio;

        [Tooltip("Sound to use when putting the item into a pot or on a stove. Leave empty for a sensible default.")]
        public string StartCookingAudio;
    }
}