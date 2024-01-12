using Extention;
using Infrastructure.ECS.Components;
using Leopotam.EcsLite;

namespace Infrastructure.ECS.Systems
{
    public class EnemyCollisionSystem : IEcsInitSystem,IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _collisionFilter;
        private EcsFilter _projectileFilter;
        private EcsPool<EnemyCollisionComponent> _collisionPool;
        private EcsPool<ProjectileComponent> _projectilePool;

        private int _perDamageRequest;
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _collisionFilter = _world.Filter<EnemyCollisionComponent>().End();
            _collisionPool = _world.GetPool<EnemyCollisionComponent>();
            _projectilePool = _world.GetPool<ProjectileComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _collisionFilter)
            {
                ref var enemyComponent = ref _collisionPool.Get(entity);
                bool isEnter = enemyComponent.collision.IsEnter;

                if (isEnter)
                {
                    enemyComponent.collision.entryEntity.Unpack(_world, out int enterEntity);
                    enemyComponent.collision.IsEnter = false;

                    DamageRequest(enterEntity, enemyComponent);
                }
            }
        }

        private void DamageRequest(int enterEntity, EnemyCollisionComponent enemyComponent)
        {
            if (_projectilePool.Has(enterEntity))
            {
                int damageRequest = _world.NewEntity();
                var from = _projectilePool.Get(enterEntity);

                _world.AddComponentToEntity(damageRequest, new DamageRequestComponent()
                {
                    packEntity = enemyComponent.enemy.PackedEntity,
                    damage = from.projectile.Damage
                });

                from.projectile.DisableBullet();
                _world.DelEntity(enterEntity);
            }
        }
    }
}