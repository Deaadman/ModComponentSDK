using ModComponent.Utilities;
using UnityEngine;

namespace ModComponent.Components
{
    [HelpURL("https://github.com/dommrogers/ModComponent/blob/master/docs/Generic-Component-Documentation.md")]
    public class ModGenericComponent : MonoBehaviour
    {
        [Tooltip("Localization key to be used for the in-game name of the item.")]
        public string DisplayNameLocalizationId;

        [Tooltip("Localization key to be used for the in-game description of the item.")]
        public string DescriptionLocalizatonId;

        [Tooltip("Localization key to be used for the 'Action' (e.g., 'Equip', 'Eat', ...) button in the inventory. The text is purely cosmetic and will not influence the action the button triggers. Leave empty for a sensible default.")]
        public string InventoryActionLocalizationId;

        [Tooltip("The weight of the item in kilograms.")]
        public float WeightKG = 1;

        [Tooltip("The number of days it takes for this item to decay - without use - from 100% to 0%. Leave at 0 if the item should not decay over time.")]
        public int DaysToDecay;

        [Tooltip("The number of hit points an item has. May affect the percent condition lost in a struggle.")]
        public float MaxHP = 100;

        [Tooltip("The initial condition of the item when found or crafted.")]
        public InitialCondition initialCondition = InitialCondition.Random;

        [Tooltip("The inventory category to be used for this item. Leave at Auto for a sensible default.")]
        public InventoryCategory inventoryCategory = InventoryCategory.Auto;

        [Tooltip("Sound to play when the item is picked up.")]
        public string PickUpAudio;

        [Tooltip("Sound to play when the item is dropped.")]
        public string PutBackAudio;

        [Tooltip("Sound to play when the item is holstered.")]
        public string StowAudio = "Play_InventoryStow";

        [Tooltip("Sound to play when the item wore out during an action.")]
        public string WornOutAudio;

        [Tooltip("Will the item be inspected when picked up? If not enabled, the item will go straight to the inventory.")]
        public bool InspectOnPickup = true;

        [Tooltip("Distance from the camera during inspect.")]
        public float InspectDistance = 0.4f;

        [Tooltip("Rotation angles (in degrees) during inspect. Use [0, 0, 0] for no rotation.")]
        public Vector3 InspectAngles;

        [Tooltip("Offset from the center during inspect. Use [0, 0, 0] for no offset.")]
        public Vector3 InspectOffset;

        [Tooltip("Scales the item during inspect. Use [1, 1, 1] for no scaling.")]
        public Vector3 InspectScale = new(1, 1, 1);

        [Tooltip("Model to show when not inspecting the item. Leave empty to have the normal model and inspect model be the same.")]
        public string NormalModel;

        [Tooltip("Model to show when inspecting the item. Leave empty to have the normal model and inspect model be the same.")]
        public string InspectModel;
    }
}