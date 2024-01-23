using ModComponent.Common;
using UnityEngine;

namespace ModComponent.Components
{
    public class ModGenericComponent : MonoBehaviour
    {
        [Header("General")]
        [Tooltip("Localization key to be used for the in-game name of the item.")]
        public string DisplayNameLocalizationId;
        [Tooltip("Localization key to be used for the in-game description of the item.")]
        public string DescriptionLocalizatonId;
        [Tooltip("Localization key to be used for the 'Action' button in the inventory. The text is purely cosmetic and will not influence the action the button triggers. Leave empty for a sensible default.")]
        public string InventoryActionLocalizationId;
        [Tooltip("The weight of the item in kilograms.")]
        public float WeightKG = 1;
        [Tooltip("The number of days it takes for this item to decay from 100% to 0%. Leave at 0 if the item should not decay over time.")]
        public int DaysToDecay;
        [Tooltip("The maximum hit points of the item.")]
        public float MaxHP = 100;
        [Tooltip("The initial condition of the item when found or crafted.")]
        public InitialCondition initialCondition = InitialCondition.Random;
        [Tooltip("The inventory category to be used for this item. Leave at 'Auto' for a sensible default.")]
        public InventoryCategory inventoryCategory = InventoryCategory.Auto;

        [Header("Audio")]
        [Tooltip("Sound to play when the item is picked up.")]
        public string PickUpAudio;
        [Tooltip("Sound to play when the item is dropped.")]
        public string PutBackAudio;
        [Tooltip("Sound to play when the item is holstered.")]
        public string StowAudio = "Play_InventoryStow";
        [Tooltip("Sound to play when the item wore out during an action.")]
        public string WornOutAudio;

        [Header("Inspect")]
        [Tooltip("Will the item be inspected when picked up? If not enabled, the item will go straight to the inventory.")]
        public bool InspectOnPickup = true;
        [Tooltip("Distance from the camera during inspect.")]
        public float InspectDistance = 0.4f;
        [Tooltip("Scales the item during inspect.")]
        public Vector3 InspectAngles;
        [Tooltip("Each vector component stands for a rotation by the given degrees around the corresponding axis.")]
        public Vector3 InspectOffset;
        [Tooltip("Offset from the center during inspect.")]
        public Vector3 InspectScale = new(1, 1, 1);
        [Tooltip("Model to show during inspect mode. NOTE: You must either set BOTH models or NO models.")]
        public string NormalModel;
        [Tooltip("Model to show when not inspect mode. NOTE: You must either set BOTH models or NO models.")]
        public string InspectModel;
    }
}