using Infrastructure.ECS;
using Infrastructure.Factories;
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
            BindPlayerSettings();
            
            BulletFactoryBinding();

            BulletPoolBinding();

            PlayerFactoryBinding();

            BindEcsSystems();

            BindEcs();
            
            Debug.Log("BindingComplete");
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

        private void BindPlayerSettings()
        {
            Container.Bind<PlayerSettingsSO>().FromScriptableObject(_playerSettings).AsSingle();
        }

        private void BindEcs()
        {
            Container.BindInterfacesAndSelfTo<EcsStartup>().AsSingle();
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

    }
}
