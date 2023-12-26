using System;
using Infrastructure.ECS.Components;
using Leopotam.EcsLite;
using Settings.Weapons;
using UnityEngine;

namespace Infrastructure.ECS.Systems
{
    [Serializable]
    public struct WeaponHolderComponent
    {
        public bool IsBusy;
        public Transform holderTransform;
    }
}