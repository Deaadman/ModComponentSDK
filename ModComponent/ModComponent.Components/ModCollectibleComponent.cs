using UnityEngine;

namespace ModComponent.Components
{
    [HelpURL("https://github.com/dommrogers/ModComponent/blob/master/docs/Collectible-Component-Documentation.md")]
    public class ModCollectibleComponent : ModGenericComponent
    {
        [Tooltip("The localization id for the hud message displayed after this item is picked up.")]
        public string HudMessageLocalizationId;

        [Tooltip("The localization id for the narrative content of the item.")]
        public string NarrativeTextLocalizationId;

        [Tooltip("The alignment of the narrative text.")]
        public Utilities.TextAlignment TextAlignment;
    }
}