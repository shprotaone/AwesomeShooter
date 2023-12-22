using System;
using Infrastructure.ECS.Components;
using Infrastructure.ECS.Services;
using Leopotam.EcsLite;

namespace Infrastructure.ECS.Systems
{
    public class ReloadMagazineSystem : IEcsInitSystem,IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _magazineFilter;
        private EcsFilter _weaponFilter;
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
            _magazinePool = _world.GetPool<AmmoMagazineComponent>();
            _weaponFilter = _world.Filter<WeaponComponent>().End();
            _weaponPool = _world.GetPool<WeaponComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var weapon in _weaponFilter)
            {
                ref var current = ref _weaponPool.Get(weapon);
                if (current.isEquipped)
                {
                    _weaponEntity = weapon;
                }
            }
        }

        public void Reload()
        {
            foreach (int entity in _weaponFilter)
            {
                ref var currentAmmo = ref _magazinePool.Get(entity);
                currentAmmo._currentAmmo = currentAmmo._maxCapacity;
            }
        }
    }
}