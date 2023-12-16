using Infrastructure.ECS.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Infrastructure.ECS.Systems
{
    public class LifetimeSystem : IEcsInitSystem,IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _lifetimeFilter;
        private EcsPool<LifeTimeComponent> _lifetimePool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _lifetimeFilter = _world.Filter<LifeTimeComponent>().End();
            _lifetimePool = _world.GetPool<LifeTimeComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _lifetimeFilter)
            {
                ref var lifetime = ref _lifetimePool.Get(entity).lifeTime;
                lifetime -= Time.deltaTime;
            }
        }
    }
}