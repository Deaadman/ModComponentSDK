using ModComponent.SDK.Components;
using UnityEngine;

namespace ModComponent.Components
{
    [HelpURL("https://github.com/dommrogers/ModComponent/blob/master/docs/Charcoal-Component-Documentation.md")]
    public class ModCharcoalComponent : ModGenericComponent
    {
        public float SurveyGameMinutes;
        public float SurveyRealSeconds;
        public float SurveySkillExtendedHours;
        public DataSoundAsset SurveyLoopAudio;
    }
}