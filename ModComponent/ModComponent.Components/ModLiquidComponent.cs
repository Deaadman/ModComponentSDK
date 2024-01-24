using ModComponent.Utilities;
using UnityEngine;

namespace ModComponent.Components
{
    [HelpURL("https://github.com/dommrogers/ModComponent/blob/master/docs/Liquid-Component-Documentation.md")]
    public class ModLiquidComponent : ModGenericComponent
    {
        [Tooltip("What type of liquid does this container hold?")]
        public LiquidType LiquidType;

        [Tooltip("The maximum capacity (in liters) of the container.")]
        public float LiquidCapacityLiters;

        [Tooltip("Should the amount be randomized?")]
        public bool RandomizedQuantity;

        [Tooltip("The initial amount of liquid this container contains. Does nothing if the quantity is randomized.")]
        public float LiquidLiters;
    }
}