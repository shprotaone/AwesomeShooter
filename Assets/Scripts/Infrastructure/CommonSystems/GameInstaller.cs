using Infrastructure.AssetManagment;
using Infrastructure.ECS.Services;
using Infrastructure.Factories;
using Infrastructure.SceneManagment;
using Infrastructure.Services;
using Infrastructure.StateMachines;
using Zenject;

namespace Infrastructure.CommonSystems
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindGameStateMachine();

            BindAssetProvider();

            BindStatesFactory();
        
            BindCommonSystemFactory();

            BindSceneLoader();

            BindLoadingCurtain();
            
            BindInputService();

            BindCursorLockService();

            BindTimeService();

        }

        private void BindTimeService()
        {
            Container.BindInterfacesAndSelfTo<TimeService>().AsSingle();
        }

        private void BindCursorLockService()
        {
            Container.BindInterfacesAndSelfTo<CursorLockService>().AsSingle();
        }


        private void BindLoadingCurtain()
        {
            Container.BindInterfacesAndSelfTo<LoadingCurtainProxy>().AsSingle();
        }
        
        private void BindSceneLoader()
        {
            Container.BindInterfacesTo<SceneLoader>().AsSingle();
        }

        private void BindCommonSystemFactory()
        {
            Container.BindInterfacesAndSelfTo<CommonSystemsFactory>().AsSingle();
        }

        private void BindStatesFactory()
        {
            Container.Bind<StatesFactory>().AsSingle();
        }

        private void BindAssetProvider()
        {
            Container.BindInterfacesTo<AssetProvider>().AsSingle();
        }

        private void BindGameStateMachine() => 
            Container.Bind<GameStateMachine>().AsSingle();
        
        private void BindInputService()
        {
            Container.Bind<InputService>().AsSingle();
        }
    }
}