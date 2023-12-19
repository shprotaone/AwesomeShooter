using Infrastructure.ECS.Components;
using Leopotam.EcsLite;

namespace Infrastructure.ECS.Systems
{
    public class ObstacleCollisionCheckSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _obstacleFilter;
        private EcsFilter _projectileFilter;
        private EcsPool<ObstacleCollisionComponent> _obstaclePool;
        private EcsPool<ProjectileComponent> _projectilePool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _obstacleFilter = _world.Filter<ObstacleCollisionComponent>().End();
            _obstaclePool = _world.GetPool<ObstacleCollisionComponent>();
            _projectilePool = _world.GetPool<ProjectileComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _obstacleFilter)
            {
                ref var obstacleComponent = ref _obstaclePool.Get(entity);
                bool isEnter = obstacleComponent.collision.IsEnter;

                if (isEnter)
                {
                    obstacleComponent.collision.entryEntity.Unpack(_world,out int bullet);
                    obstacleComponent.collision.IsEnter = false;
                    if (_projectilePool.Has(bullet))
                    {
                        var projectile = _projectilePool.Get(bullet);
                        projectile.projectile.DisableBullet();
                        _world.DelEntity(bullet);
                    }

                }
            }
        }

    }
}