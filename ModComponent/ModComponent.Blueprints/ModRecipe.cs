using ModComponent.SDK.Components;
using UnityEngine;

namespace ModComponent.Blueprints
{
    [HelpURL("https://github.com/Deaadman/ModComponentSDK/wiki/API#modrecipe")]
    public class ModRecipe : ModBlueprint
    {
        [Tooltip("The name of the recipe.")]
        public string RecipeName;

        [Tooltip("A description of the recipe.")]
        public string RecipeDescription;

        [Tooltip("Icon representing the recipe.")]
        public DataGearAsset RecipeIcon;

        [Range(0, 5)]
        [Tooltip("Minimum skill level required to use the recipe.")]
        public int RequiredSkillLevel;

        [Tooltip("Specific cooking pots allowed for this recipe.")]
        public DataGearAsset[] AllowedCookingPots;

        [Tooltip("Powder ingredients required, including type and amount in kg.")]
        public PowderRequirement[] RequiredPowder;

        [Tooltip("Liquid ingredients required, including type and volume in liters.")]
        public LiquidRequirement[] RequiredLiquid;
    }

    [System.Serializable]
    public class PowderRequirement
    {
        [Tooltip("Type of powder ingredient.")]
        public DataPowderAsset PowderItem;

        [Tooltip("Amount needed in kilograms.")]
        public int QuantityInKilograms;
    }

    [System.Serializable]
    public class LiquidRequirement
    {
        [Tooltip("Type of liquid ingredient.")]
        public DataLiquidAsset LiquidItem;

        [Tooltip("Volume needed in liters.")]
        public int VolumeInLitres;
    }
}