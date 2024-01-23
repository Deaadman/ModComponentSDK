using UnityEngine;

namespace ModComponent.Behaviours
{
    [HelpURL("https://github.com/dommrogers/ModComponent/blob/master/docs/Stackable-Behaviour-Documentation.md")]
    public class ModStackableBehaviour : MonoBehaviour
    {
        public string SingleUnitTextId;
        public string MultipleUnitTextId;
        public string StackSprite;
        public int UnitsPerItem;
        public float ChanceFull;
        public string[] ShareStackWithGear ;
        public string InstantiateStackItem;
        public float StackConditionDifferenceConstraint;
    }
}