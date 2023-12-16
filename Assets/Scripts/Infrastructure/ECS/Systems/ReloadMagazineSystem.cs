using System;
using Infrastructure.ECS.Services;
using Leopotam.EcsLite;

namespace Infrastructure.ECS.Systems
{
    public class ReloadMagazineSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private EcsFilter _magazineFilter;
        private EcsPool<AmmoMagazineComponent> _magazinePool;
        private EcsPool<WeaponComponent> _weaponPool;

        private InputService _inputService;
        private int _weaponEntity;

        public ReloadMagazineSystem(InputService inputService)
        {
            _inputService = inputService;
            _inputService.OnReload += Reload;
        }

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _magazineFilter = _world.Filter<AmmoMagazineComponent>().End();
            _magazinePool = _world.GetPool<AmmoMagazineComponent>();
            _weaponPool = _world.GetPool<WeaponComponent>();
        }

        public void SetWeaponEntity(int entity)
        {
            _weaponEntity = entity;
        }

        public void Reload()
        {
            foreach (int entity in _magazineFilter)
            {
                ref var currentAmmo = ref _magazinePool.Get(entity)._currentAmmo;
                currentAmmo = ref _weaponPool.Get(_weaponEntity).settings.magazineCapacity;
            }
        }
    }
}