using Extention;
using Infrastructure.ECS.Components;
using Infrastructure.Services;
using Leopotam.EcsLite;

namespace Infrastructure.ECS.Systems
{
    public class DeathSystem : IEcsInitSystem,IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        
        private EcsPool<DeathRequestComponent> _deathEventPool;
        private EcsPool<DeathComponent> _deathComponentPool;
        private EcsPool<ExperienceStorageComponent> _expStorageComponent;
        private ILevelProgressService _levelProgressService;
        public DeathSystem(ILevelProgressService levelProgressService)
        {
            _levelProgressService = levelProgressService;
        }

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<DeathRequestComponent>().End();
            _deathEventPool = _world.GetPool<DeathRequestComponent>();
            _deathComponentPool = _world.GetPool<DeathComponent>();
            _expStorageComponent = _world.GetPool<ExperienceStorageComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int deathRequest in _filter)
            {
                ref var packedEntity = ref _deathEventPool.Get(deathRequest).packedEntity;
                packedEntity.Unpack(_world, out int entity);
                ref var deathComponent = ref _deathComponentPool.Get(entity).OnDeath;
                
                deathComponent?.Invoke();
                _deathEventPool.Del(deathRequest);
                
                AddExperience(entity);
                UpdateProgress(); //TODO:Проверка что умер именно Enemy
                _world.DelEntity(entity);
            }
        }

        private void UpdateProgress()
        {
            _levelProgressService.DecreaseLeftEnemy();
        }

        private void AddExperience(int entity)
        {
            ref var expStorageComponent = ref _expStorageComponent.Get(entity);
            int expRequest = _world.NewEntity();

            _world.AddComponentToEntity(expRequest, new AddExperienceRequestComponent()
            {
                experience = expStorageComponent.value
            });
        }
    }
}