using ModComponent.Utilities;
using UnityEngine;

namespace ModComponent.Behaviours
{
    [HelpURL("https://github.com/dommrogers/ModComponent/blob/master/docs/Scent-Behaviour-Documentation.md")]
    public class ModScentBehaviour : MonoBehaviour
    {
        [Tooltip("What type of smell does this item have? Affects wildlife detection radius and smell strength.")]
        public ScentCategory ScentCategory;
    }
}