using System;
using System.Collections.Generic;
using Extention;
using Infrastructure.CommonSystems;
using Infrastructure.ECS.Components;
using Infrastructure.ECS.Components.Tags;
using Infrastructure.Services;
using Leopotam.EcsLite;
using MonoBehaviours;
using MonoBehaviours.Interfaces;
using Objects;
using UnityEngine.AI;

namespace Infrastructure.ECS.Systems
{
    public class EnemySpawnSystem : IEcsInitSystem,IEcsRunSystem
    {
        private EcsWorld _ecsWorld;
        private EnemyPool _pool;
        private IGameSceneData _gameSceneData;

        private TimeService _timeService;
        private float _timeToNextSpawn;

        public EnemySpawnSystem(EnemyPool pool,
            ILevelSettingsLoader levelSettingsLoader,
            TimeService timeService)
        {
            _pool = pool;
            _gameSceneData = levelSettingsLoader.GameSceneData;
            _timeService = timeService;
            _timeToNextSpawn = 0.5f;
        }

        public void Init(IEcsSystems systems)
        {
            _ecsWorld = systems.GetWorld();
            _gameSceneData = systems.GetShared<IGameSceneData>();

            foreach (EnemySpawnPoint point in _gameSceneData.SpawnEnemiesPoints)
            {
                SpawnEnemy(point);
            }
        }

        public void Run(IEcsSystems systems)
        {
            _timeToNextSpawn -= _timeService.DeltaTime;

            if (_timeToNextSpawn < 0)
            {
                _timeToNextSpawn = 0.5f; //TODO: Вынести в настройки
                EnemySpawnPoint nextPoint = GetRandomPoint();
                SpawnEnemy(nextPoint);
            }
        }

        private EnemySpawnPoint GetRandomPoint()
        {
            Random rnd = new Random();
            int nextIndex = rnd.Next(0, _gameSceneData.SpawnEnemiesPoints.Length);
            return _gameSceneData.SpawnEnemiesPoints[nextIndex];
        }

        private void SpawnEnemy(EnemySpawnPoint point)
        {
            if (point.IsActive)
            {
                Enemy enemy = _pool.Pool.Get();
                //TODO: жизни должны заполняться
                enemy.transform.position = point.Transform.position;
                var componentList = CreateComponents(enemy);

                int entity = _ecsWorld.NewEntityWithComponents(componentList);
                enemy.SetPackedEntity(_ecsWorld.PackEntity(entity));
            }
        }

        private List<object> CreateComponents(Enemy enemy)
        {
            List<object> components = new List<object>();

            components.Add(new EnemyTag());
            components.Add(new EnemyCollisionComponent()
            {
                collision = enemy.GetComponent<CollisionEvent>(),
                enemy = enemy
            });

            components.Add(new HealthComponent()
            {
                health = enemy.EnemySettings.Health
            });

            components.Add(new DeathComponent()
            {
                OnDeath = enemy.DisableObj
            });
            
            components.Add(new DamageComponent()
            {
                value = enemy.EnemySettings.Damage
            });
            
            components.Add(new ExperienceStorageComponent()
            {
                value = 10
            });

            components.Add(new EnemyMovableComponent()
            {
                speed = enemy.EnemySettings.Speed,
                agent = enemy.GetComponent<NavMeshAgent>()
            });

            return components;
        }
    }
}