using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagment;
using Infrastructure.CommonSystems;
using UnityEngine;
using Zenject;

namespace Infrastructure.Factories
{
    public class CommnonSystemsFactory
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
    }
}