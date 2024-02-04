using ModComponent.SDK.Components;
using UnityEngine;

namespace ModComponent.Blueprints
{
    [HelpURL("")]
    public class ModRecipe : ModBlueprint
    {
        public string RecipeName;
        public string RecipeDescription;
        public DataGearAsset RecipeIcon;
        public int RequiredSkillLevel;
        public DataGearAsset[] AllowedCookingPots;

        public PowderRequirement[] RequiredPowder;
        public LiquidRequirement[] RequiredLiquid;
    }

    [System.Serializable]
    public class PowderRequirement
    {
        public DataPowderAsset PowderItem;
        public int QuantityInKilograms;
    }

    [System.Serializable]
    public class LiquidRequirement
    {
        public DataLiquidAsset LiquidItem;
        public int VolumeInLitres;
    }
}