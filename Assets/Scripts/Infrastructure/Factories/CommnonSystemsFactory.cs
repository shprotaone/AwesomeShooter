using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagment;
using Infrastructure.CommonSystems;
using MonoBehaviours;
using MonoBehaviours.Interfaces;
using UnityEngine;
using Zenject;

namespace Infrastructure.Factories
{
    public class CommnonSystemsFactory : ICommonSystemsFactory
    {
        private IInstantiator _instantiator;
        private IAssetProvider _assetProvider;
        private DiContainer _container;

        [Inject]
        private void Construct(IInstantiator instantiator,IAssetProvider assetProvider,DiContainer container)
        {
            _instantiator = instantiator;
            _assetProvider = assetProvider;
            _container = container;
        }

        public async UniTask<LoadingCurtain> InitializeCurtainLoadingAsync()
        {
            GameObject prefab = await _assetProvider.Load<GameObject>(AssetAddress.LoadingCurtainPath);
            GameObject newObj = _instantiator.InstantiatePrefab(prefab);
            LoadingCurtain curtain = newObj.GetComponent<LoadingCurtain>();
            _container.Rebind<ILoadingCurtain>().To<LoadingCurtain>().FromInstance(curtain).AsSingle();
            return curtain;
        }

        public async UniTask<IGameSceneData> GetGameSceneData()
        {
            GameObject prefab = await _assetProvider.Load<GameObject>(AssetAddress.FirstLevelGameSceneDataPath);
            return prefab.GetComponent<GameSceneData>();
        }
    }
}