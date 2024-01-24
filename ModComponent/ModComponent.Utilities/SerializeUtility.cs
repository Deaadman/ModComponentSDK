using ModComponent.Behaviours;
using ModComponent.Components;

namespace ModComponent.Utilities
{
    internal class SerializeUtility
    {
        internal static object SerializeComponent(object component)
        {
            return component switch
            {
                ModGenericComponent modGenericComponent => new
                {
                    modGenericComponent.DisplayNameLocalizationId,
                    modGenericComponent.DescriptionLocalizatonId,
                    modGenericComponent.InventoryActionLocalizationId,
                    modGenericComponent.WeightKG,
                    modGenericComponent.DaysToDecay,
                    modGenericComponent.MaxHP,
                    InitialCondition = modGenericComponent.InitialCondition.ToString(),
                    InventoryCategory = modGenericComponent.InventoryCategory.ToString(),
                    modGenericComponent.PickUpAudio,
                    modGenericComponent.PutBackAudio,
                    modGenericComponent.StowAudio,
                    modGenericComponent.WornOutAudio,
                    modGenericComponent.InspectOnPickup,
                    modGenericComponent.InspectDistance,
                    InspectAngles = new float[] { modGenericComponent.InspectAngles.x, modGenericComponent.InspectAngles.y, modGenericComponent.InspectAngles.z },
                    InspectOffset = new float[] { modGenericComponent.InspectOffset.x, modGenericComponent.InspectOffset.y, modGenericComponent.InspectOffset.z },
                    InspectScale = new float[] { modGenericComponent.InspectScale.x, modGenericComponent.InspectScale.y, modGenericComponent.InspectScale.z },
                    modGenericComponent.NormalModel,
                    modGenericComponent.InspectModel
                },
                ModAccelerantBehaviour modAccelerantBehaviour => new
                {
                    modAccelerantBehaviour.DestroyedOnUse,
                    modAccelerantBehaviour.DurationOffset,
                    modAccelerantBehaviour.SuccessModifier
                },
                ModBurnableBehaviour modBurnableBehaviour => new
                {
                    modBurnableBehaviour.BurningMinutes,
                    modBurnableBehaviour.BurningMinutesBeforeAllowedToAdd,
                    modBurnableBehaviour.SuccessModifier,
                    modBurnableBehaviour.TempIncrease,
                    modBurnableBehaviour.DurationOffset
                },
                ModCarryingCapacityBehaviour modCarryingCapacityBehaviour => new
                {
                    modCarryingCapacityBehaviour.MaxCarryCapacityKGBuff
                },
                ModEvolveBehaviour modEvolveBehaviour => new
                {
                    modEvolveBehaviour.TargetItemName,
                    modEvolveBehaviour.EvolveHours,
                    modEvolveBehaviour.IndoorsOnly
                },
                ModFireStarterBehaviour modFireStarterBehaviour => new
                {
                    modFireStarterBehaviour.DestroyedOnUse,
                    modFireStarterBehaviour.NumberOfUses,
                    modFireStarterBehaviour.OnUseSoundEvent,
                    modFireStarterBehaviour.RequiresSunLight,
                    modFireStarterBehaviour.RuinedAfterUse,
                    modFireStarterBehaviour.SecondsToIgniteTinder,
                    modFireStarterBehaviour.SecondsToIgniteTorch,
                    modFireStarterBehaviour.SuccessModifier
                },
                ModHarvestableBehaviour modHarvestableBehaviour => new
                {
                    modHarvestableBehaviour.Audio,
                    modHarvestableBehaviour.Minutes,
                    modHarvestableBehaviour.YieldCounts,
                    modHarvestableBehaviour.YieldNames,
                    modHarvestableBehaviour.RequiredToolNames
                },
                ModMillableBehaviour modMillableBehaviour => new
                {
                    modMillableBehaviour.RepairDurationMinutes,
                    modMillableBehaviour.RepairRequiredGear,
                    modMillableBehaviour.RepairRequiredGearUnits,
                    modMillableBehaviour.CanRestoreFromWornOut,
                    modMillableBehaviour.RecoveryDurationMinutes,
                    modMillableBehaviour.RestoreRequiredGear,
                    modMillableBehaviour.RestoreRequiredGearUnits,
                    modMillableBehaviour.Skill
                },
                ModRepairableBehaviour modRepairableBehaviour => new
                {
                    modRepairableBehaviour.Audio,
                    modRepairableBehaviour.Minutes,
                    modRepairableBehaviour.Condition,
                    modRepairableBehaviour.RequiredTools,
                    modRepairableBehaviour.MaterialNames,
                    modRepairableBehaviour.MaterialCounts
                },
                ModScentBehaviour modScentBehaviour => new
                {
                    ScentCategory = modScentBehaviour.ScentCategory.ToString()
                },
                ModSharpenableBehaviour modSharpenableBehaviour => new
                {
                    modSharpenableBehaviour.Audio,
                    modSharpenableBehaviour.MinutesMin,
                    modSharpenableBehaviour.MinutesMax,
                    modSharpenableBehaviour.ConditionMin,
                    modSharpenableBehaviour.ConditionMax,
                    modSharpenableBehaviour.Tools
                },
                ModStackableBehaviour modStackableBehaviour => new
                {
                    modStackableBehaviour.SingleUnitTextId,
                    modStackableBehaviour.MultipleUnitTextId,
                    modStackableBehaviour.StackSprite,
                    modStackableBehaviour.UnitsPerItem,
                    modStackableBehaviour.ChanceFull,
                    modStackableBehaviour.ShareStackWithGear,
                    modStackableBehaviour.InstantiateStackItem,
                    modStackableBehaviour.StackConditionDifferenceConstraint
                },
                ModTinderBehaviour modTinderBehaviour => new
                {
                    modTinderBehaviour.DurationOffset,
                    modTinderBehaviour.SuccessModifier
                },
                _ => component
            };
        }
    }
}