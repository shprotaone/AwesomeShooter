using Infrastructure;
using Infrastructure.AssetManagment;
using Infrastructure.CommonSystems;
using Infrastructure.ECS.Services;
using Infrastructure.Factories;
using Infrastructure.GameStates;
using UnityEngine;
using Zenject;

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

    }

    private void BindLoadingCurtain()
    {
        Container.Bind<ILoadingCurtain>().To<LoadingCurtainMock>().AsCached();
    }

    private void BindSceneLoader()
    {
        Container.BindInterfacesTo<SceneLoader>().AsSingle();
    }

    private void BindCommonSystemFactory()
    {
        Container.Bind<CommnonSystemsFactory>().AsSingle();
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
}