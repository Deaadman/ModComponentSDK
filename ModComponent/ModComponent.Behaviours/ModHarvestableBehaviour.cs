using UnityEngine;

namespace ModComponent.Behaviours
{
    [HelpURL("https://github.com/dommrogers/ModComponent/blob/master/docs/Harvestable-Behaviour-Documentation.md")]
    public class ModHarvestableBehaviour : MonoBehaviour
    {
        [Tooltip("The audio to play while harvesting.")]
        public string Audio;

        [Tooltip("How many in-game minutes does it take to harvest this item?")]
        public int Minutes;

        [Tooltip("The numbers of each Gear Item that harvesting will yield.")]
        public int[] YieldCounts;

        [Tooltip("The names of the Gear Items that harvesting will yield.")]
        public string[] YieldNames;

        [Tooltip("The names of the ToolItems that can be used to harvest. Leave empty for harvesting by hand.")]
        public string[] RequiredToolNames;
    }
}