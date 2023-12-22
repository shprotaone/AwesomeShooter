using Infrastructure.ECS;
using Infrastructure.Factories;
using Infrastructure.StateMachines;
using Settings;
using UnityEngine;
using Zenject;

namespace Infrastructure.CommonSystems
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindWeaponFactory();

            BulletFactoryBinding();

            BulletPoolBinding();

            PlayerFactoryBinding();

            BindStatesFactory();

            BindSceneStateMachine();

            BindLevelSettings();

            BindEnemyFactory();

            BindEcsSystems();

            BindEcsRunner();

            BindRestartService();

            Debug.Log("BindingComplete");
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
            Container.BindInterfacesAndSelfTo<LevelSettingsLoader>().AsSingle();
        }

    }
}
