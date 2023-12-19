using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagment;
using Infrastructure.ECS;
using Infrastructure.GameStates;
using Infrastructure.SceneManagment;
using UnityEngine;
using Zenject;

namespace Infrastructure.Bootstrappers
{
    public class GameplayState : IState
    {
        private IInstantiator _instantiator;
        private IAssetProvider _assetProvider;
        private ISceneLoader _sceneLoader;
        private PlayerInitSystem _playerInit;
        private EcsStartup _ecsStartup;
        
        public GameplayState(ISceneLoader sceneLoader, IAssetProvider assetProvider,IInstantiator instantiator)
        {
            _sceneLoader = sceneLoader;
            _assetProvider = assetProvider;
            _instantiator = instantiator;
        }

        public async UniTask Enter()
        {
            await _sceneLoader.Load(AssetAddress.GameplayScenePath);
            // GameObject ecsStartupPrefab = await _assetProvider.Load<GameObject>(AssetAddress.ECSStartUp);
            // GameObject instance = _instantiator.InstantiatePrefab(ecsStartupPrefab);
            // _ecsStartup = instance.GetComponent<EcsStartup>();
            // _playerInit.Init(_ecsStartup.World);
            // await _ecsStartup.Init();
        }

        public async UniTask Exit()
        {
            Debug.Log("Exit "+ this.GetType());
        }
    }
}