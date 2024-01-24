using UnityEngine;

namespace ModComponent.Behaviours
{
    [HelpURL("https://github.com/dommrogers/ModComponent/blob/master/docs/Accelerant-Behaviour-Documentation.md")]
    public class ModAccelerantBehaviour : MonoBehaviour
    {
        [Tooltip("Is the item destroyed immediately after use?")]
        public bool DestroyedOnUse;

        [Tooltip("In-game seconds offset for fire starting duration from this accelerant. NOT scaled by fire starting skill. Positive values mean 'slower', negative values mean 'faster'.")]
        public float DurationOffset;

        [Tooltip("Does this item affect the chance for success? Represents percentage points. Positive values increase the chance, negative values reduce it.")]
        public float SuccessModifier;
    }
}