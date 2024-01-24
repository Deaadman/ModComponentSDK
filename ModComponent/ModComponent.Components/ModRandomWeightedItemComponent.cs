using UnityEngine;

namespace ModComponent.Components
{
    [HelpURL("https://github.com/dommrogers/ModComponent/blob/master/docs/Random-Weighted-Item-Component-Documentation.md")]
    public class ModRandomWeightedItemComponent : ModGenericComponent
    {
        [Tooltip("The names of the gear items that this could spawn. Must be the same length as ItemWeights")]
        public string[] ItemNames;

        [Tooltip("The integer weights of the gear items that this could spawn. Must be the same length as ItemNames")]
        public int[] ItemWeights;
    }
}