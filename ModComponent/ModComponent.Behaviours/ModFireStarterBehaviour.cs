using UnityEngine;

namespace ModComponent.Behaviours
{
    [HelpURL("https://github.com/dommrogers/ModComponent/blob/master/docs/FireStarter-Behaviour-Documentation.md")]
    public class ModFireStarterBehaviour : ModBaseBehaviour
    {
        [Tooltip("Is the item destroyed immediately after use?")]
        public bool DestroyedOnUse;

        [Tooltip("How many times can this item be used?")]
        public float NumberOfUses;

        [Tooltip("What sound to play during usage.")]
        public string OnUseSoundEvent;

        [Tooltip("Does the item require sunlight to work?")]
        public bool RequiresSunLight;

        [Tooltip("Set the condition to 0% after the fire starting finished (either successful or not).")]
        public bool RuinedAfterUse;

        [Tooltip("How many in-game seconds this item will take to ignite tinder.")]
        public float SecondsToIgniteTinder;

        [Tooltip("How many in-game seconds this item will take to ignite a torch.")]
        public float SecondsToIgniteTorch;

        [Tooltip("Does this item affect the chance for success? Represents percentage points. Positive values increase the chance, negative values reduce it.")]
        public float SuccessModifier;
    }
}