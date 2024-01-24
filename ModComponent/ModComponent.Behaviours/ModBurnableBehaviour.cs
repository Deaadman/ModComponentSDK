using UnityEngine;

namespace ModComponent.Behaviours
{
    [HelpURL("https://github.com/dommrogers/ModComponent/blob/master/docs/Burnable-Behaviour-Documentation.md")]
    public class ModBurnableBehaviour : ModBaseBehaviour
    {
        [Tooltip("Number of minutes this item adds to the remaining burn time of the fire.")]
        public int BurningMinutes;

        [Tooltip("How long must a fire be burning before this item can be added?")]
        public float BurningMinutesBeforeAllowedToAdd;

        [Tooltip("Does this item affect the chance for successfully starting a fire? Represents percentage points. Positive values increase the chance, negative values reduce it.")]
        public float SuccessModifier;

        [Tooltip("Temperature increase when added to the fire.")]
        public float TempIncrease;

        [Tooltip("In-game seconds offset for fire starting duration from this accelerant. NOT scaled by fire starting skill. Positive values mean 'slower', negative values mean 'faster'.")]
        public float DurationOffset;
    }
}