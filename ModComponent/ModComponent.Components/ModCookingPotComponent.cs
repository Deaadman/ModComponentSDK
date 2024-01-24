using UnityEngine;

namespace ModComponent.Components
{
    [HelpURL("https://github.com/dommrogers/ModComponent/blob/master/docs/Cooking-Pot-Component-Documentation.md")]
    public class ModCookingPotComponent : ModGenericComponent
    {
        [Tooltip("Can the item cook liquids?")]
        public bool CanCookLiquid;

        [Tooltip("Can the item cook grub? Cookable canned food counts as grub.")]
        public bool CanCookGrub;

        [Tooltip("Can the item cook meat?")]
        public bool CanCookMeat;

        [Tooltip("The total water capacity of the item.")]
        public float Capacity;

        [Tooltip("Template item to be used in the mapping process.")]
        public string Template = "";
    }
}