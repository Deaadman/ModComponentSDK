using ModComponent.Utilities;
using UnityEngine;

namespace ModComponent.Components
{
    [HelpURL("https://github.com/dommrogers/ModComponent/blob/master/docs/Powder-Component-Documentation.md")]
    public class ModPowderComponent : ModGenericComponent
    {
        [Tooltip("The type of powder this container holds. Gunpowder is the only option right now.")]
        public PowderType PowderType;

        [Tooltip("The maximum weight this container can hold.")]
        public float CapacityKG;

        [Tooltip("The percent probability that this container will be found full.")]
        [Range(0, 100)]
        public float ChanceFull;
    }
}