using ModComponent.SDK.Components;
using UnityEngine;

namespace ModComponent.Behaviours
{
    [HelpURL("https://github.com/dommrogers/ModComponent/blob/master/docs/Stackable-Behaviour-Documentation.md")]
    public class ModStackableBehaviour : ModBaseBehaviour
    {
        [Tooltip("Localization key to be used for stacks with only one item. E.g. '2 arrows'.")]
        public string SingleUnitTextId;

        [Tooltip("Localization key to be used for stacks with multiple items. E.g. '2 arrows'.")]
        public string MultipleUnitTextId;

        [Tooltip("An optional sprite name (from a UIAtlas) that will be added to the stack.")]
        public string StackSprite;

        [Tooltip("The default number of units to make a full stack. For example, Coffee tins and Herbal Tea boxes each have 5 units.")]
        public int UnitsPerItem;

        [Tooltip("Percent chance of the item having a full stack.")]
        public float ChanceFull;

        [Tooltip("The items that can be stacked with this item.")]
        public DataGearAsset[] ShareStackWithGear;

        [Tooltip("The item to instantiate when the stack is split.")]
        public DataGearAsset InstantiateStackItem;

        [Tooltip("The maximum difference in condition between items in a stack.")]
        public float StackConditionDifferenceConstraint;
    }
}