using ModComponent.SDK.Components;
using UnityEngine;

namespace ModComponent.Components
{
    [HelpURL("https://github.com/dommrogers/ModComponent/blob/master/docs/Bed-Component-Documentation.md")]
    public class ModBedComponent : ModGenericComponent
    {
        [Tooltip("How many condition points are restored per hour by sleeping in this bed? This is the base rate and applied for the first hour. The second and following hours will benefit from 'AdditionalConditionGainPerHour'.")]
        public float ConditionGainPerHour;

        [Tooltip("Additionally restored condition points restored per hour. The n-th hour of sleeping gives (n - 1) * AdditionalConditionGainPerHour additional health points.")]
        public float AdditionalConditionGainPerHour;

        [Tooltip("Warmth bonus of the bed.")]
        public float WarmthBonusCelsius;

        [Tooltip("How much condition does this bed item lose per hour of use?")]
        public float DegradePerHour;

        [Tooltip("Modifier for the chance of bear encounters during sleep. Positive values decrease the chance; negative values increase the chance.")]
        public float BearAttackModifier;

        [Tooltip("Modifier for the chance of wolf encounters during sleep. Positive values decrease the chance; negative values increase the chance.")]
        public float WolfAttackModifier;

        [Tooltip("Sound to be played when beginning to sleep in this bed. Leave empty for a sensible default.")]
        public DataSoundAsset OpenAudio;

        [Tooltip("Sound to be played when ending to sleep in this bed. Leave empty for a sensible default.")]
        public DataSoundAsset CloseAudio;

        [Tooltip("Optional game object to be used for representing the bed in a 'packed' state.")]
        public GameObject PackedMesh;

        [Tooltip("Optional game object to be used for representing the bed in a 'usable' state.")]
        public GameObject UsableMesh;
    }
}