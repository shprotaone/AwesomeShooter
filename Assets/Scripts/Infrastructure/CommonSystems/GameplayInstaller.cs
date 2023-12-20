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
        [SerializeField] private PlayerSettingsSO _playerSettings;
        
        public override void InstallBindings()
        {
            BindWeaponFactory();
            
            BindPlayerSettings();
            
            BulletFactoryBinding();

            BulletPoolBinding();

            PlayerFactoryBinding();

            BindEcsSystems();
            
            BindStatesFactory();

            BindSceneStateMachine();
            
            BindLevelSettings();

            //BindEcs();

            Debug.Log("BindingComplete");
        }

        private void BindStatesFactory()
        {
            Container.BindInterfacesAndSelfTo<StatesFactory>().AsSingle();
        }

        private void BindSceneStateMachine()
        {
            Container.Bind<SceneStateMachine>().AsSingle();
        }

        private void BindEcs()
        {
            Container.BindInterfacesAndSelfTo<EcsStartup>().AsSingle();
        }

        private void BindWeaponFactory()
        {
            Container.BindInterfacesAndSelfTo<WeaponFactory>().AsSingle();
        }

        private void BindEcsSystems()
        {
            // Container.BindInterfacesAndSelfTo<EcsUpdateSystems>()
            //     .FromSubContainerResolve()
            //     .ByInstaller<ECSUpdateSystemsInstaller>().AsSingle();
            //
            // Container.BindInterfacesAndSelfTo<EcsFixedSystems>()
            //     .FromSubContainerResolve()
            //     .ByInstaller<ECSFixedUpdateSystemsInstaller>().AsSingle();
        }

        private void BindPlayerSettings()
        {
            Container.Bind<PlayerSettingsSO>().FromScriptableObject(_playerSettings).AsSingle();
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
