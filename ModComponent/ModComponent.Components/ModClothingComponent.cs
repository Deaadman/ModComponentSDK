using ModComponent.Utilities;
using UnityEngine;

namespace ModComponent.Components
{
    [HelpURL("https://github.com/dommrogers/ModComponent/blob/master/docs/Clothing-Component-Documentation.md")]
    public class ModClothingComponent : ModGenericComponent
    {
        [Tooltip("Region where this clothing can be worn.")]
        public ClothingRegion Region;

        [Tooltip("The innermost layer at which the clothing item can be worn (Base, Mid, Top, or Top2).")]
        public ClothingLayer MinLayer;

        [Tooltip("The outermost layer at which the clothing item can be worn (Base, Mid, Top, or Top2).")]
        public ClothingLayer MaxLayer;

        [Tooltip("The type of sound made when moving while wearing this clothing item.")]
        public ClothingMovementSound MovementSound;

        [Tooltip("The type of footwear this clothing item represents.")]
        public FootwearType Footwear;

        [Tooltip("Number of days it takes for this clothing item to decay from 100% to 0% while being worn outside.")]
        public float DaysToDecayWornOutside;

        [Tooltip("Number of days it takes for this clothing item to decay from 100% to 0% while being worn inside.")]
        public float DaysToDecayWornInside;

        [Tooltip("Warmth bonus in degrees Celsius when the clothing item is in perfect condition and completely dry.")]
        public float Warmth;

        [Tooltip("Warmth bonus in degrees Celsius when the clothing item is in perfect condition and completely wet.")]
        public float WarmthWhenWet;

        [Tooltip("Windproof bonus in degrees Celsius when the clothing item is in perfect condition and completely wet.")]
        public float Windproof;

        [Tooltip("How much water is repelled by this clothing item.")]
        public float Waterproofness;

        [Tooltip("Damage reduction in percent when receiving certain types of damage.")]
        public float Toughness;

        [Tooltip("Sprint stamina reduction in percent.")]
        public float SprintBarReduction;

        [Range(0, 100)]
        [Tooltip("Decreases the chance that a wolf will attack. Only applies in certain situations. 100 means 'guaranteed not to attack'; 0 means 'same as without the buff'.")]
        public int DecreaseAttackChance;

        [Range(0, 100)]
        [Tooltip("Increases the chance that a wolf will flee immediately when spotting the player. 100 means 'guaranteed to flee'; 0 means 'same as without the buff'.")]
        public int IncreaseFleeChance;

        [Tooltip("Hours required to dry this clothing item next to a fire when it is completely wet.")]
        public float HoursToDryNearFire;

        [Tooltip("Hours required to dry this clothing item without a fire when it is completely wet.")]
        public float HoursToDryWithoutFire;

        [Tooltip("Hours required for this clothing to completely freeze once it gets wet.")]
        public float HoursToFreeze;

        [Tooltip("Base name of the texture to represent this clothing item in the paper doll view.")]
        public string MainTexture;

        [Tooltip("Name of the blend texture used for the paper doll view.")]
        public string BlendTexture;

        [Tooltip("Drawing layer (drawing order) to be used for this clothing item.")]
        public int DrawLayer;

        [Tooltip("The name of the type implementing the specific game logic of this item.")]
        public string ImplementationType;
    }
}