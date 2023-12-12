using System;
using Infrastructure.ECS.Components;
using Infrastructure.ECS.Components.Providers;
using Leopotam.EcsLite;
using UnityEngine;

namespace Infrastructure.ECS.Systems
{
    public class GravitySystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _gravityFilter;
        private EcsPool<GravityComponent> _gravityPool;
        private EcsPool<MovableComponent> _movablePool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _gravityFilter = _world.Filter<GravityComponent>().Inc<MovableComponent>().End();
            _gravityPool = _world.GetPool<GravityComponent>();
            _movablePool = _world.GetPool<MovableComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _gravityFilter)
            {
                ref var gravityComponent = ref _gravityPool.Get(entity);
                ref var movableComponent = ref _movablePool.Get(entity);

                ref var velocity = ref movableComponent.velocity;
                velocity.y += -gravityComponent.gravity * Time.deltaTime;
            }
        }
    }
}