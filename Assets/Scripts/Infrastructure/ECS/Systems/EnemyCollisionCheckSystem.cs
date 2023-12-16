using Infrastructure.ECS.Components;
using Leopotam.EcsLite;

namespace Infrastructure.ECS.Systems
{
    public class EnemyCollisionCheckSystem : IEcsInitSystem,IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _obstacleFilter;
        private EcsFilter _projectileFilter;
        private EcsPool<EnemyCollisionComponent> _obstaclePool;
        private EcsPool<ProjectileComponent> _projectilePool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _obstacleFilter = _world.Filter<EnemyCollisionComponent>().End();
            _obstaclePool = _world.GetPool<EnemyCollisionComponent>();
            _projectilePool = _world.GetPool<ProjectileComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _obstacleFilter)
            {
                ref var enemyComponent = ref _obstaclePool.Get(entity);
                bool isEnter = enemyComponent.collision.IsEnter;

                if (isEnter)
                {
                    enemyComponent.collision.entryEntity.Unpack(_world,out int bullet);
                    enemyComponent.collision.IsEnter = false;
                    var projectile = _projectilePool.Get(bullet);
                    enemyComponent.enemy.GetDamage(projectile.projectile.Damage);
                    projectile.projectile.DisableBullet();
                    _world.DelEntity(bullet);
                }
            }
        }
    }
}