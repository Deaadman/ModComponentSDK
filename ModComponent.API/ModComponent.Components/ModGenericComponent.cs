using UnityEngine;

namespace Deadman.ModComponent.Components
{
    public class ModGenericComponent : MonoBehaviour
    {
        public string DisplayNameLocalizationId;
        public string DescriptionLocalizatonId;
        public string InventoryActionLocalizationId;
        public float WeightKG;
        public int DaysToDecay;
        public float MaxHP;
        public string InitialCondition;
        public string InventoryCategory;
        public string PickUpAudio;
        public string PutBackAudio;
        public string StowAudio;
        public string WornOutAudio;
        public bool InspectOnPickup;
        public float InspectDistance;
        public Vector3 InspectAngles;
        public Vector3 InspectOffset;
        public Vector3 InspectScale;
        public string NormalModel;
        public string InspectModel;
    }
}