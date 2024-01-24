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
                ModAmmoComponent modAmmoComponent => new
                {
                    modAmmoComponent.DisplayNameLocalizationId,
                    modAmmoComponent.DescriptionLocalizatonId,
                    modAmmoComponent.InventoryActionLocalizationId,
                    modAmmoComponent.WeightKG,
                    modAmmoComponent.DaysToDecay,
                    modAmmoComponent.MaxHP,
                    InitialCondition = modAmmoComponent.InitialCondition.ToString(),
                    InventoryCategory = modAmmoComponent.InventoryCategory.ToString(),
                    modAmmoComponent.PickUpAudio,
                    modAmmoComponent.PutBackAudio,
                    modAmmoComponent.StowAudio,
                    modAmmoComponent.WornOutAudio,
                    modAmmoComponent.InspectOnPickup,
                    modAmmoComponent.InspectDistance,
                    InspectAngles = new float[] { modAmmoComponent.InspectAngles.x, modAmmoComponent.InspectAngles.y, modAmmoComponent.InspectAngles.z },
                    InspectOffset = new float[] { modAmmoComponent.InspectOffset.x, modAmmoComponent.InspectOffset.y, modAmmoComponent.InspectOffset.z },
                    InspectScale = new float[] { modAmmoComponent.InspectScale.x, modAmmoComponent.InspectScale.y, modAmmoComponent.InspectScale.z },
                    modAmmoComponent.NormalModel,
                    modAmmoComponent.InspectModel,

                    AmmoForGunType = modAmmoComponent.AmmoForGunType.ToString()
                },
                ModBedComponent modBedComponent => new
                {
                    modBedComponent.DisplayNameLocalizationId,
                    modBedComponent.DescriptionLocalizatonId,
                    modBedComponent.InventoryActionLocalizationId,
                    modBedComponent.WeightKG,
                    modBedComponent.DaysToDecay,
                    modBedComponent.MaxHP,
                    InitialCondition = modBedComponent.InitialCondition.ToString(),
                    InventoryCategory = modBedComponent.InventoryCategory.ToString(),
                    modBedComponent.PickUpAudio,
                    modBedComponent.PutBackAudio,
                    modBedComponent.StowAudio,
                    modBedComponent.WornOutAudio,
                    modBedComponent.InspectOnPickup,
                    modBedComponent.InspectDistance,
                    InspectAngles = new float[] { modBedComponent.InspectAngles.x, modBedComponent.InspectAngles.y, modBedComponent.InspectAngles.z },
                    InspectOffset = new float[] { modBedComponent.InspectOffset.x, modBedComponent.InspectOffset.y, modBedComponent.InspectOffset.z },
                    InspectScale = new float[] { modBedComponent.InspectScale.x, modBedComponent.InspectScale.y, modBedComponent.InspectScale.z },
                    modBedComponent.NormalModel,
                    modBedComponent.InspectModel,

                    modBedComponent.ConditionGainPerHour,
                    modBedComponent.AdditionalConditionGainPerHour,
                    modBedComponent.WarmthBonusCelsius,
                    modBedComponent.DegradePerHour,
                    modBedComponent.BearAttackModifier,
                    modBedComponent.WolfAttackModifier,
                    modBedComponent.OpenAudio,
                    modBedComponent.CloseAudio,
                    modBedComponent.PackedMesh,
                    modBedComponent.UsableMesh
                },
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