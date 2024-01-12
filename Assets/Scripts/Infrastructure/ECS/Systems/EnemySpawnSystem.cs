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
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

namespace Infrastructure.ECS.Systems
{
    public class EnemySpawnSystem : IEcsInitSystem,IEcsRunSystem
    {
        private EcsWorld _ecsWorld;
        private EnemyPool _prefabPool;
        private ILevelData _levelData;

        private EcsFilter _enemiesFilter;
        private TimeService _timeService;
        private ILevelProgressService _levelProgressService;
        private float _timeToNextSpawn;

        public EnemySpawnSystem(EnemyPool prefabPool,
            TimeService timeService,
            ILevelProgressService levelProgressService)
        {
            _prefabPool = prefabPool;
            _timeService = timeService;
            _levelProgressService = levelProgressService;
        }

        public void Init(IEcsSystems systems)
        {
            _ecsWorld = systems.GetWorld();
            _levelData = systems.GetShared<ILevelData>();
            
            _timeToNextSpawn = _levelData.LevelSettings.minSpawnTimeRate;
            _levelProgressService.SetEnemiesInLevel(_levelData.LevelSettings.enemiesOnLevel);
            
            _enemiesFilter = _ecsWorld.Filter<EnemyTag>().End();

            foreach (EnemySpawnPoint point in _levelData.SpawnEnemiesPoints)
            {
                SpawnEnemy(point);
            }
        }

        public void Run(IEcsSystems systems)
        {
            _timeToNextSpawn -= _timeService.DeltaTime;

            if (_timeToNextSpawn < 0 && CheckMaxEnemiesOnMap() && _levelProgressService.LeftEnemy > 0)
            {
                _timeToNextSpawn = _levelData.LevelSettings.minSpawnTimeRate;
                EnemySpawnPoint nextPoint = GetRandomPoint();
                SpawnEnemy(nextPoint);
            }
        }

        private EnemySpawnPoint GetRandomPoint()
        {
            Random rnd = new Random();
            int nextIndex = rnd.Next(0, _levelData.SpawnEnemiesPoints.Length);
            return _levelData.SpawnEnemiesPoints[nextIndex];
        }

        private void SpawnEnemy(EnemySpawnPoint point)
        {
            if (point.IsActive)
            {
                Enemy enemy = _prefabPool.Pool.Get();
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

        private bool CheckMaxEnemiesOnMap()
        {
            return _levelData.LevelSettings.maxEnemiesOnMap >= _enemiesFilter.GetEntitiesCount();
        }
    }
}