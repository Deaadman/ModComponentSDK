using ModComponent.Behaviours;
using ModComponent.Components;

namespace ModComponent.Utilities
{
    internal class SerializeUtility
    {
        internal static object SerializeComponent(object component)
        {
            if (component is ModGenericComponent modGenericComponent)
            {
                return new
                {
                    modGenericComponent.DisplayNameLocalizationId,
                    modGenericComponent.DescriptionLocalizatonId,
                    modGenericComponent.InventoryActionLocalizationId,
                    modGenericComponent.WeightKG,
                    modGenericComponent.DaysToDecay,
                    modGenericComponent.MaxHP,
                    InitialCondition = modGenericComponent.initialCondition.ToString(),
                    InventoryCategory = modGenericComponent.inventoryCategory.ToString(),
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
                };
            }

            if (component is ModStackableBehaviour mmodStackableBehaviour)
            {
                return new
                {
                    mmodStackableBehaviour.SingleUnitTextId,
                    mmodStackableBehaviour.MultipleUnitTextId,
                    mmodStackableBehaviour.StackSprite,
                    mmodStackableBehaviour.UnitsPerItem,
                    mmodStackableBehaviour.ChanceFull,
                    mmodStackableBehaviour.ShareStackWithGear,
                    mmodStackableBehaviour.InstantiateStackItem,
                    mmodStackableBehaviour.StackConditionDifferenceConstraint,
                };
            }

            return null;
        }
    }
}