using System;
using Infrastructure.ECS.Components.Providers;
using Leopotam.EcsLite;
using Settings.Weapons;
using UnityEngine;
using Voody.UniLeo.Lite;

namespace Infrastructure.ECS.Views
{
    public class WeaponConfig : MonoBehaviour
    {
        [SerializeField] private Transform _muzzleTransform;
        [SerializeField] private WeaponSettings _weaponSettings;
        public Transform MuzzleTransform => _muzzleTransform;
        public WeaponSettings WeaponSettings => _weaponSettings;

        public EcsPackedEntity entity { get; set; }
    }
}