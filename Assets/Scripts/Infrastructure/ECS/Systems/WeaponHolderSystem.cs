using System;
using Infrastructure.ECS.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Infrastructure.ECS.Systems
{
    public class WeaponHolderSystem : IEcsInitSystem,IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _pickUpRequest;
        private EcsFilter _weaponHolderFilter;
        private EcsPool<WeaponHolderComponent> _weaponsPool;
        private EcsPool<PickUpRequest> _requestPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _pickUpRequest = _world.Filter<PickUpRequest>().End();
            _weaponsPool = _world.GetPool<WeaponHolderComponent>();
            _requestPool = _world.GetPool<PickUpRequest>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int request in _pickUpRequest)
            {
                ref var requestComponent = ref _requestPool.Get(request);

                if (requestComponent.itemType == ItemType.WEAPON)
                {
                    var weaponHolder = FindHolder();
                    requestComponent.entity.Unpack(_world,out int weapon);
                    Debug.Log(weapon);
                }
            }
        }

        private WeaponHolderComponent FindHolder()
        {
            foreach (int holder in _weaponHolderFilter)
            {
                return _weaponsPool.Get(holder);
            }

            return default;
        }
    }
}