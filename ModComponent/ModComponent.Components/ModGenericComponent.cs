using ModComponent.SDK.Components;
using ModComponent.Utilities;
using UnityEngine;

namespace ModComponent.Components
{
    [HelpURL("https://github.com/dommrogers/ModComponent/blob/master/docs/Generic-Component-Documentation.md")]
    [DisallowMultipleComponent]
    public class ModGenericComponent : MonoBehaviour
    {
        [Tooltip("Localization key to be used for the in-game name of the item.")]
        public string DisplayNameLocalizationId = "GAMEPLAY_NameKey";

        [Tooltip("Localization key to be used for the in-game description of the item.")]
        public string DescriptionLocalizatonId = "GAMEPLAY_DescriptionKey";

        [Tooltip("Localization key to be used for the 'Action' (e.g., 'Equip', 'Eat', ...) button in the inventory. The text is purely cosmetic and will not influence the action the button triggers. Leave empty for a sensible default.")]
        public string InventoryActionLocalizationId;

        [Tooltip("The weight of the item in kilograms.")]
        public float WeightKG = 1;

        [Tooltip("The number of days it takes for this item to decay - without use - from 100% to 0%. Leave at 0 if the item should not decay over time.")]
        public int DaysToDecay;

        [Tooltip("The number of hit points an item has. May affect the percent condition lost in a struggle.")]
        public float MaxHP = 100;

        [Tooltip("The initial condition of the item when found or crafted.")]
        public InitialCondition InitialCondition;

        [Tooltip("The inventory category to be used for this item. Leave at Auto for a sensible default.")]
        public InventoryCategory InventoryCategory;

        [Tooltip("Sound to play when the item is picked up.")]
        public DataSoundAsset PickUpAudio;

        [Tooltip("Sound to play when the item is dropped.")]
        public DataSoundAsset PutBackAudio;

        [Tooltip("Sound to play when the item is holstered.")]
        public DataSoundAsset StowAudio;

        [Tooltip("Sound to play when the item wore out during an action.")]
        public DataSoundAsset WornOutAudio;

        [Tooltip("Will the item be inspected when picked up? If not enabled, the item will go straight to the inventory.")]
        public bool InspectOnPickup = true;

        [Tooltip("Distance from the camera during inspect.")]
        public float InspectDistance = 1;

        [Tooltip("Rotation angles (in degrees) during inspect. Use [0, 0, 0] for no rotation.")]
        public Vector3 InspectAngles;

        [Tooltip("Offset from the center during inspect. Use [0, 0, 0] for no offset.")]
        public Vector3 InspectOffset;

        [Tooltip("Scales the item during inspect. Use [1, 1, 1] for no scaling.")]
        public Vector3 InspectScale = new(1, 1, 1);

        [Tooltip("Model to show when not inspecting the item. Leave empty to have the normal model and inspect model be the same.")]
        public GameObject NormalModel;

        [Tooltip("Model to show when inspecting the item. Leave empty to have the normal model and inspect model be the same.")]
        public GameObject InspectModel;
    }
}