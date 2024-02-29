using Cysharp.Threading.Tasks;
using Infrastructure.ECS;
using Infrastructure.Factories;
using Infrastructure.Services;
using Infrastructure.StateMachines;
using Scripts.Test;
using Settings;
using UI;
using UnityEngine;
using Zenject;

namespace Infrastructure.CommonSystems
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindPauseService();
            
            BindServiceInitializer();
            
            BindWeaponFactory();

            BulletFactoryBinding();

            BulletPoolBinding();

            BindServices();

            PlayerFactoryBinding();

            BindLevelSettings();

            BindStatesFactory();

            BindSceneStateMachine();

            BindEnemyFactory();

            BindUIFactory();

            BindEcsSystems();

            BindEcsRunner();

            BindRestartService();

            BindTestService();
        }

        private void BindTestService()
        {
            Container.BindInterfacesAndSelfTo<TestService>().AsSingle();
        }

        private void BindServiceInitializer()
        {
            Container.BindInterfacesAndSelfTo<ServiceInitializer>().AsSingle();
        }

        private void BindUIFactory()
        {
            Container.BindInterfacesAndSelfTo<UIService>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameplayUIFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<MainHUDController>().AsSingle();
        }

        private void BindEnemyFactory()
        {
            Container.BindInterfacesAndSelfTo<EnemyFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyPool>().AsSingle();
        }

        private void BindEcsRunner()
        {
            Container.BindInterfacesAndSelfTo<EcsStartup>().AsSingle();
        }

        private void BindRestartService()
        {
            Container.BindInterfacesAndSelfTo<RestartService>().AsSingle();
        }

        private void BindStatesFactory()
        {
            Container.BindInterfacesAndSelfTo<StatesFactory>().AsSingle();
        }

        private void BindSceneStateMachine()
        {
            Container.Bind<SceneStateMachine>().AsSingle();
        }

        private void BindWeaponFactory()
        {
            Container.BindInterfacesAndSelfTo<WeaponFactory>().AsSingle();
        }

        private void BindEcsSystems()
        {
            Container.BindInterfacesAndSelfTo<EcsUpdateSystems>()
                .FromSubContainerResolve()
                .ByInstaller<ECSUpdateSystemsInstaller>().AsSingle();

            Container.BindInterfacesAndSelfTo<EcsFixedSystems>()
                .FromSubContainerResolve()
                .ByInstaller<ECSFixedUpdateSystemsInstaller>().AsSingle();
        }

        private void PlayerFactoryBinding()
        {
            Container.BindInterfacesAndSelfTo<PlayerFactory>().AsSingle();
        }

        private void BulletPoolBinding()
        {
            Container.Bind<BulletPool>().AsSingle();
        }

        private void BulletFactoryBinding()
        {
            Container.Bind<BulletFactory>().AsSingle();
        }
        
        private void BindLevelSettings()
        {
            Container.BindInterfacesAndSelfTo<LevelSettingsContainer>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelSettingsLoader>().AsSingle();
        }

        private void BindServices()
        {
            Container.BindInterfacesAndSelfTo<LevelProgressGameService>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelingGameService>().AsSingle();
            Container.BindInterfacesAndSelfTo<SkillGameService>().AsSingle();
        }

        private void BindPauseService()
        {
            Container.BindInterfacesAndSelfTo<PauseGameService>().AsSingle();
        }
    }
}
