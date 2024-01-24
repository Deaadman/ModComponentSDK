using ModComponent.Utilities;
using UnityEngine;

namespace ModComponent.Components
{
    [HelpURL("https://github.com/dommrogers/ModComponent/blob/master/docs/Ammo-Component-Documentation.md")]
    public class ModAmmoComponent : ModGenericComponent
    {
        [Tooltip("The type of gun item which determines what the ammo is used for.")]
        public AmmoForGunType AmmoForGunType;
    }
}