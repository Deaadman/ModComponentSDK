using ModComponent.SDK.Components;
using UnityEngine;

namespace ModComponent.Behaviours
{
    [HelpURL("https://github.com/dommrogers/ModComponent/blob/master/docs/Repairable-Behaviour-Documentation.md")]
    public class ModRepairableBehaviour : ModBaseBehaviour
    {
        [Tooltip("The audio to play while repairing.")]
        public string Audio;

        [Tooltip("How many in-game minutes does it take to repair this item?")]
        public int Minutes;

        [Tooltip("How much condition does repairing restore?")]
        public int Condition;

        [Tooltip("The name of the tools suitable for repair. At least one of those will be required for repairing. Leave empty if this item should be repairable without tools.")]
        public GearAsset[] RequiredTools;

        [Tooltip("The names of the materials required for repair.")]
        public GearAsset[] MaterialNames;

        [Tooltip("The number of materials required for repair.")]
        public int[] MaterialCounts;
    }
}