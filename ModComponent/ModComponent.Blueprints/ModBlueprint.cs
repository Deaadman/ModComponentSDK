using ModComponent.SDK.Components;
using ModComponent.Utilities;
using UnityEngine;

namespace ModComponent.Blueprints
{
    [HelpURL("https://github.com/Deaadman/ModComponentSDK/wiki/API#modblueprint")]
    [DisallowMultipleComponent]
    public class ModBlueprint : MonoBehaviour
    {
        [Tooltip("Optional blueprint name for error logging.")]
        public string Name;

        [Tooltip("Required items and quantities for crafting.")]
        public GearRequirement[] RequiredGear;

        [Tooltip("Kerosene needed (liters).")]
        public float KeroseneLitersRequired;

        [Tooltip("Gunpowder needed (kg).")]
        public float GunpowderKGRequired;

        [Tooltip("Mandatory tool for crafting.")]
        public DataGearAsset RequiredTool;

        [Tooltip("Optional tools to improve or substitute crafting.")]
        public DataGearAsset[] OptionalTools;

        [Tooltip("Crafting location requirement.")]
        public WorkbenchType RequiredCraftingLocation;

        [Tooltip("Need a lit fire to craft?")]
        public bool RequiresLitFire;

        [Tooltip("Need light to craft?")]
        public bool RequiresLight;

        [Tooltip("Crafting result item.")]
        public DataGearAsset CraftedResult;

        [Tooltip("Amount produced.")]
        public int CraftedResultCount;

        [Tooltip("Crafting time (minutes).")]
        public int DurationMinutes;

        [Tooltip("Sound effect during crafting.")]
        public DataSoundAsset CraftingAudio;

        [Tooltip("Skill associated with crafting.")]
        public SkillType AppliedSkill;

        [Tooltip("Skill improved upon successful crafting.")]
        public SkillType ImprovedSkill;
    }

    [System.Serializable]
    public class GearRequirement
    {
        [Tooltip("Required gear item name.")]
        public DataGearAsset GearItem;

        [Tooltip("Quantity required.")]
        public int Count;
    }
}