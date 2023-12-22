using System.Collections.Generic;
using Extention;
using Infrastructure.CommonSystems;
using Infrastructure.ECS.Components;
using Infrastructure.ECS.Components.Tags;
using Infrastructure.ECS.Views;
using Leopotam.EcsLite;
using MonoBehaviours;
using MonoBehaviours.Interfaces;
using UnityEngine;
using Zenject;

namespace Infrastructure.ECS.Systems
{
    public class WeaponSpawnSystem : IEcsInitSystem
    {
        private EcsWorld _ecsWorld;
        private IInstantiator _instatiator;
        private IWeaponFactory _weaponFactory;
        private IGameSceneData _gameSceneData;

        public WeaponSpawnSystem(IWeaponFactory weaponFactory,
            ILevelSettingsLoader levelSettingsLoader,
            IInstantiator instantiator)
        {
            _weaponFactory = weaponFactory;
            _gameSceneData = levelSettingsLoader.GameSceneData;
            _instatiator = instantiator;
        }
        
        public void Init(IEcsSystems systems)
        {
            _ecsWorld = systems.GetWorld();
            _gameSceneData = systems.GetShared<IGameSceneData>();
            //по количеству точек
            foreach (WeaponSpawnPoint point in _gameSceneData.SpawnWeaponPoints)
            {
                SpawnWeapon(point);
            }
            
        }

        private async void SpawnWeapon(WeaponSpawnPoint point)
        {
            GameObject weaponPrefab = await _weaponFactory.GetWeapon(point.WeaponType);

            weaponPrefab.transform.position = point.Transform.position;
            var weapon = weaponPrefab.GetComponent<WeaponConfig>();
            var componentList = CreateWeaponComponents(weapon);

            int entity = _ecsWorld.NewEntityWithComponents(componentList);

            _ecsWorld.AddComponentToEntity(entity, new PickUpComponent()
            {
                entity = _ecsWorld.PackEntity(entity),
                isPicked = false
            });

            weapon.entity = _ecsWorld.PackEntity(entity);
        }

        private List<object> CreateWeaponComponents(WeaponConfig weapon)
        {
            List<object> components = new List<object>();


            components.Add(new WeaponPlayerTag());

            components.Add(new WeaponComponent()
            {
                muzzle = weapon.MuzzleTransform,
                isEquipped = false,
                settings = weapon.WeaponSettings
            });

            components.Add(new FireRateComponent()
            {
                firerate = weapon.WeaponSettings.fireRate
            });

            components.Add(new ItemTypeComponent()
            {
                type = weapon.WeaponSettings.ItemType
            });

            components.Add(new ModelComponent()
            {
                modelTransform = weapon.transform
            });

            components.Add(new AmmoMagazineComponent()
            {
                _currentAmmo = weapon.WeaponSettings.magazineCapacity,
                _maxCapacity = weapon.WeaponSettings.magazineCapacity
            });

            return components;
        }
    }
}
