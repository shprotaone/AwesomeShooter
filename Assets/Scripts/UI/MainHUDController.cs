using Cysharp.Threading.Tasks;
using Infrastructure.ECS;
using Infrastructure.ECS.Components;
using Infrastructure.ECS.Components.Tags;
using Infrastructure.ECS.Systems;
using Infrastructure.Services;
using Leopotam.EcsLite;
using Scripts.Test;
using UIComponents;
using UnityEngine;
using Zenject;

namespace UI
{
    public class MainHUDController
    {
        private EcsStartup _ecsStartup;
        private UIService _uiService;

        private EcsWorld _ecsWorld;

        public MainHUDController(EcsStartup world,UIService uiService)
        {
            _ecsStartup = world;
            _uiService = uiService;
        }

        public async UniTask Initialize()
        {
            _ecsWorld = _ecsStartup.CurrentWorld;
            SetUpHp();
            SetUpExperienceWindow();
            SetUpWeapon();
            await SetUpTestServiceWindow();
        }

        private async UniTask SetUpTestServiceWindow()
        {
            await _uiService.GetView<TestServiceView>().Init();
        }

        private async void SetUpWeapon()
        {
            WeaponView view = _uiService.GetView<WeaponView>();
            view.ResetView();
        }

        private void SetUpExperienceWindow()
        {
            EcsFilter filter = _ecsWorld.Filter<ExperienceComponent>().Inc<PlayerTag>().End();
            EcsPool<ExperienceComponent> pool = _ecsWorld.GetPool<ExperienceComponent>();
            PlayerInfoView view =_uiService.GetView<PlayerInfoView>();

            foreach (int entity in filter)
            {
                ref var experienceComponent = ref pool.Get(entity);

                experienceComponent.OnExperienceAdd = view.ChangeExp;
                view.SetUpExp(experienceComponent.Level);
            }
        }

        private void SetUpHp()
        {
            EcsFilter filter = _ecsWorld.Filter<PlayerTag>().Inc<HealthComponent>().End();
            EcsPool<HealthComponent> _healthPool = _ecsWorld.GetPool<HealthComponent>();
            PlayerInfoView view =_uiService.GetView<PlayerInfoView>();

            foreach (int entity in filter)
            {
                ref var healthComponent = ref _healthPool.Get(entity);
                healthComponent.OnHealthChanged = view.ChangeHealth;
                view.SetUpHP(healthComponent.health);
            }
        }
    }
}