using ModComponent.SDK.Components;
using UnityEngine;

namespace ModComponent.Components
{
    [HelpURL("https://github.com/dommrogers/ModComponent/blob/master/docs/Generic-Equippable-Component-Documentation.md")]
    public class ModGenericEquippableComponent : ModGenericComponent
    {
        [Tooltip("The GameObject to be used for representing the item while it is equipped.")]
        public DataGearAsset EquippedModelPrefab;

        [Tooltip("The name of the type implementing the specific game logic of this item.")]
        public string ImplementationType;

        [Tooltip("The audio that plays when this item is equipped.")]
        public DataSoundAsset EquippingAudio;
    }
}