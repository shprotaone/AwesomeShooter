using Infrastructure.ECS.Components;
using Infrastructure.ECS.Components.Tags;
using Infrastructure.Services;
using Leopotam.EcsLite;

namespace Infrastructure.ECS.Systems
{
    public class EnemyDamageSystem : IEcsInitSystem,IEcsRunSystem,IPaused
    {
        private EcsWorld _world;

        private EcsFilter _playerFilter;
        private EcsFilter _movableFilter;

        private EcsPool<HealthComponent> _healthPool;
        private EcsPool<ModelComponent> _playerPool;
        private EcsPool<EnemyMovableComponent> _movablePool;

        private readonly PauseGameService _pauseGameService;

        public bool IsPaused { get; set; }

        public EnemyDamageSystem(PauseGameService pauseGameService)
        {
            pauseGameService.Add(this);
        }

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _playerFilter = _world.Filter<ModelComponent>().Inc<PlayerTag>().End();
            _movableFilter = _world.Filter<EnemyMovableComponent>().Inc<EnemyTag>().End();

            _playerPool = _world.GetPool<ModelComponent>();
            _healthPool = _world.GetPool<HealthComponent>();
            _movablePool = _world.GetPool<EnemyMovableComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            //проверяем дистанцию

            //наносим урон если дистанция подходящая

            foreach (var entity in _movableFilter)
            {
                ref var enemyMovable = ref _movablePool.Get(entity);
                ref var healthEnemy = ref _healthPool.Get(entity);


            }
        }

        /*private void CreateDamageRequest(int entity, Enemy enemy)
        {
            perDamage = _world.NewEntity();
            var to = _damagePool.Get(entity);

            _world.AddComponentToEntity(perDamage, new PeriodicDamageRequestComponent()
            {
                PackedEntity = _world.PackEntity(entity),
                Damage = to.value,
                TimeOfDamage = enemy.EnemySettings.DamageTime
            });

            _world.AddComponentToEntity(perDamage, new FireRateComponent()
            {
                firerate = enemy.EnemySettings.DamageTime
            });
        }*/


    }
}