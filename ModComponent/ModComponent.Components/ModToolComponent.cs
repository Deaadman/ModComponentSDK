using ModComponent.SDK.Components;
using ModComponent.Utilities;
using UnityEngine;

namespace ModComponent.Components
{
    [HelpURL("https://github.com/dommrogers/ModComponent/blob/master/docs/Tool-Component-Documentation.md")]
    public class ModToolComponent : ModGenericEquippableComponent
    {
        [Tooltip("The type of the tool item. This determines for which actions it can be used.")]
        public ToolKind ToolType;

        [Tooltip("None, HackSaw, Hatchet, Hammer, Knife")]
        public ToolKind ToolKind;

        [Tooltip("How many condition points per use does this tool item lose?")]
        public float DegradeOnUse;

        [Tooltip("Can this item be used for crafting, repairing, or both?")]
        public ToolUsage Usage;

        [Tooltip("Bonus to the relevant skill when using this item.")]
        public int SkillBonus;

        [Tooltip("Multiplier for crafting and repair times. Represents percent.")]
        public float CraftingTimeMultiplier;

        [Tooltip("How many condition points does the tool degrade while being used for crafting?")]
        public float DegradePerHourCrafting;

        [Tooltip("Can this tool be used to break down items?")]
        public bool BreakDown;

        [Tooltip("Multiplier for the time required to break down an item. Represents percent.")]
        public float BreakDownTimeMultiplier;

        [Tooltip("Can this tool item be used to open locked containers?")]
        public bool ForceLocks;

        [Tooltip("Sound to play while forcing a lock.")]
        public DataSoundAsset ForceLockAudio;

        [Tooltip("Can this tool item be used to clear ice fishing holes?")]
        public bool IceFishingHole;

        [Tooltip("How many condition points does the tool lose when completely clearing an ice fishing hole?")]
        public float IceFishingHoleDegradeOnUse;

        [Tooltip("How many in-game minutes does it take to completely clear an ice fishing hole?")]
        public int IceFishingHoleMinutes;

        [Tooltip("Sound to play while clearing an ice fishing hole.")]
        public DataSoundAsset IceFishingHoleAudio;

        [Tooltip("Can this tool item be used to harvest carcasses?")]
        public bool CarcassHarvesting;

        [Tooltip("How many in-game minutes does it take to harvest one kg of unfrozen meat?")]
        public int MinutesPerKgMeat;

        [Tooltip("How many in-game minutes does it take to harvest one kg of frozen meat?")]
        public int MinutesPerKgFrozenMeat;

        [Tooltip("How many in-game minutes does it take to harvest one hide?")]
        public int MinutesPerHide;

        [Tooltip("How many in-game minutes does it take to harvest one gut?")]
        public int MinutesPerGut;

        [Tooltip("How many condition points does the tool degrade while being used for harvesting carcasses?")]
        public float DegradePerHourHarvesting;

        [Tooltip("Can this tool item be used during a struggle with wildlife?")]
        public bool StruggleBonus;

        [Tooltip("Multiplier for the damage dealt.")]
        public float DamageMultiplier;

        [Tooltip("Multiplier for the chance the animal will flee (breaking the struggle before the 'struggle bar' is filled).")]
        public float FleeChanceMultiplier;

        [Tooltip("Multiplier for the amount of the 'struggle bar' that is filled with each hit.")]
        public float TapMultiplier;

        [Tooltip("Can this tool cause a puncture wound? If enabled, this will cause the animal to bleed out.")]
        public bool CanPuncture;

        [Tooltip("Multiplier for the time it takes the animal to bleed out after receiving a puncture wound.")]
        public float BleedoutMultiplier;
    }
}