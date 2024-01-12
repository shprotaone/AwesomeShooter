using Infrastructure.CommonSystems;
using Infrastructure.ECS.Components;
using Infrastructure.ECS.Components.Tags;
using Infrastructure.Services;
using Leopotam.EcsLite;
using Settings;

namespace Infrastructure.ECS.Systems
{
    public class ExperienceSystem : IEcsInitSystem,IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _experienceRequests;
        private EcsFilter _experience;
        private EcsPool<ExperienceComponent> _experiencePool;
        private EcsPool<AddExperienceRequestComponent> _experienceRequestPool;

        private ILevelingGameService _levelingGameService;
        
        public ExperienceSystem(ILevelingGameService levelingGameService)
        {
            _levelingGameService = levelingGameService;
        }
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _experience = _world.Filter<ExperienceComponent>().Inc<PlayerTag>().End();
            _experiencePool = _world.GetPool<ExperienceComponent>();
            
            _experienceRequests = _world.Filter<AddExperienceRequestComponent>().End();
            _experienceRequestPool = _world.GetPool<AddExperienceRequestComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int request in _experienceRequests)
            {
                ref var currentRequest = ref _experienceRequestPool.Get(request);
                AddExperience(currentRequest);
                _world.DelEntity(request);
            }
        }

        private void AddExperience(AddExperienceRequestComponent currentRequest)
        {
            foreach (int i in _experience)
            {
                ref var experienceComponent = ref _experiencePool.Get(i);
                ref var experienceValue = ref experienceComponent.Experience;
                experienceValue += currentRequest.experience;

                experienceComponent.OnExperienceAdd?.Invoke(experienceValue);
                _levelingGameService.CheckNextLevel(experienceValue);
            }
        }
    }
}