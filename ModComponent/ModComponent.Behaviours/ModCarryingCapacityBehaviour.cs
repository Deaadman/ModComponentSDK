using UnityEngine;

namespace ModComponent.Behaviours
{
    [HelpURL("https://github.com/dommrogers/ModComponent/blob/master/docs/Carrying-Capacity-Behaviour-Documentation.md")]
    public class ModCarryingCapacityBehaviour : MonoBehaviour
    {
        [Tooltip("The maximum buff to the carrying capacity from this item.")]
        public float MaxCarryCapacityKGBuff;
    }
}