using UnityEngine;

namespace ModComponent.Behaviours
{
    [HelpURL("https://github.com/Deaadman/ModComponentSDK/wiki/API#modcarryingcapacitybehaviour")]
    public class ModCarryingCapacityBehaviour : ModBaseBehaviour
    {
        [Tooltip("Increases max carrying capacity by this amount.")]
        public float MaxCarryCapacityKGBuff;
    }
}