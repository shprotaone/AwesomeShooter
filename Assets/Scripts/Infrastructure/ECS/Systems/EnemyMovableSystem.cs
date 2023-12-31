﻿using Infrastructure.ECS.Components;
using Infrastructure.ECS.Components.Tags;
using Leopotam.EcsLite;
using UnityEngine;

namespace Infrastructure.ECS.Systems
{
    public class EnemyMovableSystem : IEcsInitSystem,IEcsRunSystem
    {
        private EcsWorld _world;

        private EcsFilter _filter;
        private EcsFilter _playerFilter;

        private EcsPool<HealthComponent> _healthPool;
        private EcsPool<ModelComponent> _playerPool;
        private EcsPool<EnemyMovableComponent> _pool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<EnemyMovableComponent>().Inc<EnemyTag>().End();
            _pool = _world.GetPool<EnemyMovableComponent>();

            _playerFilter = _world.Filter<ModelComponent>().Inc<PlayerTag>().End();
            _playerPool = _world.GetPool<ModelComponent>();

            _healthPool = _world.GetPool<HealthComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var enemyMovable = ref _pool.Get(entity);
                ref var healthEnemy = ref _healthPool.Get(entity);

                if (healthEnemy.health > 0)
                {
                    enemyMovable.agent.destination = CheckPlayerPosition();
                }
            }
        }

        public Vector3 CheckPlayerPosition()
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