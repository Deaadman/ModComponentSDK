using ModComponent.SDK.Components;
using UnityEngine;

namespace ModComponent.Behaviours
{
    [HelpURL("https://github.com/dommrogers/ModComponent/blob/master/docs/Evolve-Behaviour-Documentation.md")]
    public class ModEvolveBehaviour : ModBaseBehaviour
    {
        [Tooltip("Name of the item into which this item will evolve. E.g. 'GEAR_GutDried'")]
        public GearAsset TargetItemName;

        [Tooltip("How many in-game hours does this item take to evolve from 0% to 100%?")]
        public int EvolveHours;

        [Tooltip("Does this item only evolve when it is stored indoors?")]
        public bool IndoorsOnly;
    }
}