using Extention;
using Infrastructure.ECS.Components;
using Leopotam.EcsLite;
using Scripts.Test;

namespace Infrastructure.ECS.Systems
{
    public class EnemyCollisionSystem : IEcsInitSystem,IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _collisionFilter;
        private EcsFilter _projectileFilter;
        private EcsPool<EnemyCollisionComponent> _collisionPool;
        private EcsPool<ProjectileComponent> _projectilePool;

        private ITestService _testService;
        private int _perDamageRequest;

        public EnemyCollisionSystem(ITestService testService)
        {
            _testService = testService;
            _testService.AddField(typeof(DamageRequestComponent).ToString());
        }

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

                if (enemyComponent.onTrigger.IsEnter == false) return;

                enemyComponent.onTrigger.entryEntity.Unpack(_world, out int enterEntity);

                DamageRequest(enterEntity, enemyComponent);

                enemyComponent.onTrigger.IsEnter = false;
            }
        }

        private void DamageRequest(int enterEntity, EnemyCollisionComponent enemyComponent)
        {
            var from = _projectilePool.Get(enterEntity);
            int damageRequest = _world.NewEntity();

            if (enemyComponent.enemy == null)
            {
                from.projectile.DisableBullet();
                _world.DelEntity(enterEntity);
                return;
            }

            _world.AddComponentToEntity(damageRequest, new DamageRequestComponent()
            {
                packEntity = enemyComponent.enemy.PackedEntity,
                damage = from.projectile.Damage
            });

            _testService.Increase<DamageRequestComponent>();

            from.projectile.DisableBullet();
            _world.DelEntity(enterEntity);

            if (_projectilePool.Has(enterEntity))
            {

            }
        }
    }
}