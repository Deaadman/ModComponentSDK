using ModComponent.SDK.Components;
using ModComponent.Utilities;
using UnityEngine;

namespace ModComponent.Components
{
    [HelpURL("https://github.com/dommrogers/ModComponent/blob/master/docs/First-Aid-Component-Documentation.md")]
    public class ModFirstAidComponent : ModGenericComponent
    {
        [Tooltip("Localization key for the progress bar message while using the item.")]
        public string ProgressBarMessage;

        [Tooltip("Localization key for indicating the possible action with this item.")]
        public string RemedyText;

        [Tooltip("Amount of condition instantly restored after using this item.")]
        public int InstantHealing;

        [Tooltip("Type of first aid provided by this item.")]
        public FirstAidType FirstAidType;

        [Tooltip("Time in seconds to use this item.")]
        public int TimeToUseSeconds;

        [Tooltip("How many items are required for one dose or application.")]
        public int UnitsPerUse;

        [Tooltip("Sound to play when using the item.")]
        public DataSoundAsset UseAudio;
    }
}