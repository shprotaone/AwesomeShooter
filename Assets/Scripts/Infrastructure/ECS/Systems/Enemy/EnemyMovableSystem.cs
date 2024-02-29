using Infrastructure.ECS.Components;
using Infrastructure.ECS.Components.Tags;
using Infrastructure.Services;
using Leopotam.EcsLite;
using UnityEngine;

namespace Infrastructure.ECS.Systems
{
    public class EnemyMovableSystem : IEcsInitSystem,IEcsRunSystem,IPaused
    {
        private EcsWorld _world;

        private EcsFilter _movableFilter;
        private EcsFilter _playerFilter;

        private EcsPool<HealthComponent> _healthPool;
        private EcsPool<ModelComponent> _playerPool;
        private EcsPool<EnemyMovableComponent> _movablePool;

        private readonly PauseGameService _pauseGameService;

        public bool IsPaused { get; set; }

        public EnemyMovableSystem(PauseGameService pauseGameService)
        {
            pauseGameService.Add(this);
        }

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _movableFilter = _world.Filter<EnemyMovableComponent>().Inc<EnemyTag>().End();
            _movablePool = _world.GetPool<EnemyMovableComponent>();

            _playerFilter = _world.Filter<ModelComponent>().Inc<PlayerTag>().End();
            _playerPool = _world.GetPool<ModelComponent>();

            _healthPool = _world.GetPool<HealthComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _movableFilter)
            {
                ref var enemyMovable = ref _movablePool.Get(entity);
                ref var healthEnemy = ref _healthPool.Get(entity);

                if (healthEnemy.health > 0)
                {
                    enemyMovable.agent.destination = CheckPlayerPosition();
                }
                
                if(IsPaused) enemyMovable.agent.isStopped = true;
                else enemyMovable.agent.isStopped = false;
                
            }
        }

        private Vector3 CheckPlayerPosition()
        {
            foreach (int i in _playerFilter)
            {
                ref var modelComponent = ref _playerPool.Get(i);
                return modelComponent.modelTransform.position;
            }

            return default;
        }
    }
}