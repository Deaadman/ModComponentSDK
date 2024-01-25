using ModComponent.SDK.Components;
using ModComponent.Utilities;
using UnityEngine;

namespace ModComponent.Components
{
    [HelpURL("https://github.com/dommrogers/ModComponent/blob/master/docs/Blueprints.md")]
    [DisallowMultipleComponent]
    public class ModBlueprint : MonoBehaviour
    {
        [Tooltip("An optional name for the blueprint. Only used in error messages.")]
        public string Name;

        [Tooltip("The name of each gear needed to craft this item (e.g., GEAR_Line).")]
        public GearAsset[] RequiredGear;

        [Tooltip("How many of each item are required? This list has to match the RequiredGear list.")]
        public int[] RequiredGearUnits;

        [Tooltip("How many liters of kerosene are required?")]
        public float KeroseneLitersRequired;

        [Tooltip("How much gunpowder is required (in kilograms)?")]
        public float GunpowderKGRequired;

        [Tooltip("Tool required to craft (e.g., GEAR_Knife).")]
        public GearAsset RequiredTool;

        [Tooltip("List of optional tools to speed up the crafting process or to use in place of the required tool.")]
        public GearAsset[] OptionalTools;

        [Tooltip("Where to craft? (Anywhere, Workbench, Forge, AmmoWorkbench)")]
        public WorkbenchType RequiredCraftingLocation;

        [Tooltip("Does it require a lit fire in the ammo workbench to craft?")]
        public bool RequiresLitFire;

        [Tooltip("Does it require light to craft?")]
        public bool RequiresLight;

        [Tooltip("The name of the item produced.")]
        public string CraftedResult;

        [Tooltip("Number of the item produced.")]
        public int CraftedResultCount;

        [Tooltip("Number of in-game minutes required.")]
        public int DurationMinutes;

        [Tooltip("Audio to be played.")]
        public string CraftingAudio;

        [Tooltip("The skill associated with crafting this item (e.g., Gunsmithing, None if not applicable).")]
        public SkillType AppliedSkill;

        [Tooltip("The skill improved on crafting success (e.g., Firestarting, CarcassHarvesting, None if not applicable).")]
        public SkillType ImprovedSkill;
    }
}