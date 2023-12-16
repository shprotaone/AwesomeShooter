using System;
using Infrastructure.ECS.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Infrastructure.ECS.Systems
{
    public class PickUpSystem : IEcsInitSystem,IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _pickUpitems;
        private EcsPool<PickUpRequest> _pickIpRequestPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _pickUpitems = _world.Filter<PickUpRequest>().End();
            _pickIpRequestPool = _world.GetPool<PickUpRequest>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _pickUpitems)
            {

            }
        }
    }
}