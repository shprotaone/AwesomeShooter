using System.Collections.Generic;
using Extention;
using Infrastructure.CommonSystems;
using Infrastructure.ECS.Components;
using Infrastructure.ECS.Components.Tags;
using Infrastructure.Factories;
using Leopotam.EcsLite;
using MonoBehaviours;
using MonoBehaviours.Interfaces;
using Objects;

namespace Infrastructure.ECS.Systems
{
    public class SpawnEnemySystem : IEcsInitSystem,IEcsRunSystem
    {
        private EcsWorld _ecsWorld;
        private EnemyPool _pool;
        private IGameSceneData _gameSceneData;

        public SpawnEnemySystem(EnemyPool pool, ILevelSettingsLoader levelSettingsLoader)
        {
            _pool = pool;
            _gameSceneData = levelSettingsLoader.GameSceneData;
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

        }

        private void SpawnEnemy(EnemySpawnPoint point)
        {
            Enemy enemy = _pool.Pool.Get();
            enemy.transform.position = point.Transform.position;
            var componentList = CreateComponents(enemy);

            int entity = _ecsWorld.NewEntityWithComponents(componentList);
            enemy.SetPackedEntity(_ecsWorld.PackEntity(entity));

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

            return components;
        }
    }
}