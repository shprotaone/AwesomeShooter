using System;
using Settings.Weapons;
using UnityEngine;

namespace Infrastructure.ECS.Components
{
    [Serializable]
    public struct WeaponComponent
    {
        public WeaponSettings settings;
        public Transform muzzle;
        public bool isEquipped;
    }
}