using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagment;
using UI;
using UnityEngine;
using Zenject;

namespace Infrastructure.Factories
{
    public class GameplayUIFactory
    {
        private IAssetProvider _assetProvider;
        private IInstantiator _instantiator;

        [Inject]
        public GameplayUIFactory(IAssetProvider assetProvider, IInstantiator instantiator)
        {
            _assetProvider = assetProvider;
            _instantiator = instantiator;
        }

        public async UniTask<MainHUDController> CreateMainHud()
        {
            GameObject hudPrefab = await _assetProvider.Load<GameObject>(AssetAddress.GameplayHUDPath);
            GameObject instance = _instantiator.InstantiatePrefab(hudPrefab);
            MainHUDController mainHUDController = instance.GetComponentInChildren<MainHUDController>();

            return mainHUDController;
        }
    }
}