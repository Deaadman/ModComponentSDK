using ModComponent.SDK.Components;
using UnityEngine;

namespace ModComponent.Behaviours
{
    [HelpURL("https://github.com/dommrogers/ModComponent/blob/master/docs/Sharpenable-Behaviour-Documentation.md")]
    public class ModSharpenableBehaviour : ModBaseBehaviour
    {
        [Tooltip("The sound to play while sharpening. Leave empty for a sensible default.")]
        public DataSoundAsset Audio;

        [Tooltip("How many in-game minutes does it take to sharpen this item at minimum skill.")]
        public int MinutesMin;

        [Tooltip("How many in-game minutes does it take to sharpen this item at maximum skill.")]
        public int MinutesMax;

        [Tooltip("How much condition is restored to this item at minimum skill.")]
        public float ConditionMin;

        [Tooltip("How much condition is restored to this item at maximum skill.")]
        public float ConditionMax;

        [Tooltip("Which tools can be used to sharpen this item, e.g., 'GEAR_SharpeningStone'. Leave empty to make this sharpenable without tools.")]
        public DataGearAsset[] Tools;
    }
}