using Infrastructure.ECS.Components;
using Infrastructure.ECS.Components.Tags;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace UI
{
    public class MainHUDController : MonoBehaviour
    {
        //TODO: Возможно стоит вынести все в отдельный компонент
        [SerializeField] private WeaponView _weaponView;
        [SerializeField] private PlayerInfoView _playerInfoView;
        [SerializeField] private LevelInfoView _levelInfoView;

        private EcsWorld _ecsWorld;
        [Inject]
        public void Construct(EcsWorld world)
        {
            _ecsWorld = world;
        }
        public void Start()
        {
            GetHpComponent();
            SetUpExperienceWindow();
        }

        private void SetUpExperienceWindow()
        {
            float exp = 0;
            EcsFilter filter = _ecsWorld.Filter<ExperienceComponent>().Inc<PlayerTag>().End();
            EcsPool<ExperienceComponent> pool = _ecsWorld.GetPool<ExperienceComponent>();

            foreach (int entity in filter)
            {
                ref var experienceComponent = ref pool.Get(entity);

                experienceComponent.OnExperienceAdd = _playerInfoView.ChangeExp;
                _playerInfoView.SetUpExp(experienceComponent.Level);
            }
        }

        private void GetHpComponent()
        {
            float health = 0;
            EcsFilter filter = _ecsWorld.Filter<PlayerTag>().Inc<HealthComponent>().End();
            EcsPool<HealthComponent> _healthPool = _ecsWorld.GetPool<HealthComponent>();
            
            foreach (int entity in filter)
            {
                ref var healthComponent = ref _healthPool.Get(entity);
                health = healthComponent.health;

                healthComponent.OnHealthChanged = _playerInfoView.ChangeHealth;
            }

            _playerInfoView.SetUpHP(health);
        }
    }
}