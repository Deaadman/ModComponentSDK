using ModComponent.SDK.Components;
using UnityEngine;

namespace ModComponent.Behaviours
{
    [HelpURL("https://github.com/dommrogers/ModComponent/blob/master/docs/Millable-Behaviour-Documentation.md")]
    public class ModMillableBehaviour : ModBaseBehaviour
    {
        [Tooltip("The number of minutes required to repair the item.")]
        public int RepairDurationMinutes;

        [Tooltip("The Gear Items required for repairing the item.")]
        public DataGearAsset[] RepairRequiredGear;

        [Tooltip("The numbers of each Gear Item required for repairing the item.")]
        public int[] RepairRequiredGearUnits;

        [Tooltip("Can the item be restored from a ruined state?")]
        public bool CanRestoreFromWornOut;

        [Tooltip("The number of minutes required to restore the item.")]
        public int RecoveryDurationMinutes;

        [Tooltip("The Gear Items required for restoring the item.")]
        public DataGearAsset[] RestoreRequiredGear;

        [Tooltip("The numbers of each Gear Item required for restoring the item.")]
        public int[] RestoreRequiredGearUnits;

        // Skills needs to be revisted, maybe turn into an enum for easier choice of which skill.
        [Tooltip("The skill associated with repairing/restoring this item.")]
        public string Skill;
    }
}