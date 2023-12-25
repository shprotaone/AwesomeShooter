using Infrastructure.ECS.Components;
using Infrastructure.ECS.Components.Tags;
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

        private PlayerLevelSettingsSO _playerLevelSettings;
        
        public ExperienceSystem(PlayerLevelSettingsSO playerLevelSettingsSo)
        {
            _playerLevelSettings = playerLevelSettingsSo;
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
                
                // for (int j = 0; j <  _playerLevelSettings._levels.Count; j++)
                // {
                //     if (_playerLevelSettings._levels[i].experienceToUp <= experienceValue)
                //     {
                //         experienceComponent.Level = _playerLevelSettings._levels[i + 1].level;
                //     }
                // }
                
                _world.DelEntity(i);

            }
        }
    }
}