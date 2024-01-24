using UnityEngine;

namespace ModComponent.Behaviours
{
    [HelpURL("https://github.com/dommrogers/ModComponent/blob/master/docs/Millable-Behaviour-Documentation.md")]
    public class ModMillableBehaviour : MonoBehaviour
    {
        [Tooltip("The number of minutes required to repair the item.")]
        public int RepairDurationMinutes;

        [Tooltip("The Gear Items required for repairing the item.")]
        public string[] RepairRequiredGear;

        [Tooltip("The numbers of each Gear Item required for repairing the item.")]
        public int[] RepairRequiredGearUnits;

        [Tooltip("Can the item be restored from a ruined state?")]
        public bool CanRestoreFromWornOut;

        [Tooltip("The number of minutes required to restore the item.")]
        public int RecoveryDurationMinutes;

        [Tooltip("The Gear Items required for restoring the item.")]
        public string[] RestoreRequiredGear;

        [Tooltip("The numbers of each Gear Item required for restoring the item.")]
        public int[] RestoreRequiredGearUnits;

        [Tooltip("The skill associated with repairing/restoring this item.")]
        public string Skill;
    }
}