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
        private LoadingCurtainProxy _curtain;

        [Inject]
        private void Construct(IInstantiator instantiator,
            IAssetProvider assetProvider,
            LoadingCurtainProxy curtain)
        {
            _instantiator = instantiator;
            _assetProvider = assetProvider;
            _curtain = curtain;
        }

        public async UniTask InitializeCurtainLoadingAsync()
        {
            GameObject prefab = await _assetProvider.Load<GameObject>(AssetAddress.LoadingCurtainPath);
            GameObject newObj = _instantiator.InstantiatePrefab(prefab);
            LoadingCurtain curtain = newObj.GetComponent<LoadingCurtain>();
            await _curtain.InitializeCurtain(curtain);
        }

        public async UniTask<IGameSceneData> GetGameSceneData()
        {
            GameObject prefab = await _assetProvider.Load<GameObject>(AssetAddress.FirstLevelGameSceneDataPath);
            return prefab.GetComponent<GameSceneData>();
        }
    }
}