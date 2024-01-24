using ModComponent.Utilities;
using UnityEngine;

namespace ModComponent.Components
{
    [HelpURL("https://github.com/dommrogers/ModComponent/blob/master/docs/Research-Component-Documentation.md")]
    public class ModResearchComponent : ModGenericComponent
    {
        public SkillType SkillType;
        public int TimeRequirementHours;
        public int SkillPoints;
        public int NoBenefitAtSkillLevel;
        public string ReadAudio;
    }
}