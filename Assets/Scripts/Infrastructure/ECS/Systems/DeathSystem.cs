using Extention;
using Infrastructure.ECS.Components;
using Leopotam.EcsLite;

namespace Infrastructure.ECS.Systems
{
    public class DeathSystem : IEcsInitSystem,IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsPool<DeathRequestComponent> _deathEventPool;
        private EcsPool<DeathComponent> _deathComponentPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<DeathRequestComponent>().End();
            _deathEventPool = _world.GetPool<DeathRequestComponent>();
            _deathComponentPool = _world.GetPool<DeathComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int deathRequest in _filter)
            {
                ref var packedEntity = ref _deathEventPool.Get(deathRequest).packedEntity;
                packedEntity.Unpack(_world, out int entity);
                ref var deathComponent = ref _deathComponentPool.Get(entity).OnDeath;
                deathComponent?.Invoke();
                _deathEventPool.Del(deathRequest);
            }
        }
    }
}