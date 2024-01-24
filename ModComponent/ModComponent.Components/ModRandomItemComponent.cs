using UnityEngine;

namespace ModComponent.Components
{
    [HelpURL("https://github.com/dommrogers/ModComponent/blob/master/docs/Random-Item-Component-Documentation.md")]
    public class ModRandomItemComponent : ModGenericComponent
    {
        [Tooltip("The names of the gear items that this could spawn.")]
        public string[] ItemNames;
    }
}