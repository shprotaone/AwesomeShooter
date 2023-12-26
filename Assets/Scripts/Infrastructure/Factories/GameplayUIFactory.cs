using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagment;
using Infrastructure.Services;
using UI;
using UIComponents;
using UnityEngine;
using Zenject;

namespace Infrastructure.Factories
{
    public class GameplayUIFactory
    {
        private IAssetProvider _assetProvider;
        private IInstantiator _instantiator;

        private Transform _mainPanelParent;
        private UIService _uiService;
        private MainHUDController _mainHUDController;

        public GameplayUIFactory(IAssetProvider assetProvider,
            IInstantiator instantiator,
            UIService uiService,
            MainHUDController mainHUDController)
        {
            _assetProvider = assetProvider;
            _instantiator = instantiator;
            _uiService = uiService;
            _mainHUDController = mainHUDController;
        }

        public async UniTask CreateMainHud()
        {
            var instance = await Load(AssetAddress.GameplayHUDPath);

            _mainPanelParent = instance.transform;
            await CreatePlayerInfo();
            await CreateLevelInfo();
            await CreateWeaponInfo();
            await _mainHUDController.Initialize();
        }

        private async UniTask CreatePlayerInfo()
        {
            var instance = await Load(AssetAddress.PlayerInfoPanelPath);
            instance.transform.SetParent(_mainPanelParent,false);
            _uiService.AddService(instance.GetComponent<PlayerInfoView>());
        }

        private async UniTask CreateLevelInfo()
        {
            var instance = await Load(AssetAddress.LevelInfoPanelPath);
            instance.transform.SetParent(_mainPanelParent,false);
            _uiService.AddService(instance.GetComponent<LevelInfoView>());
        }

        private async UniTask CreateWeaponInfo()
        {
            var instance = await Load(AssetAddress.WeaponPanelPath);
            instance.transform.SetParent(_mainPanelParent,false);
            _uiService.AddService(instance.GetComponent<WeaponView>());
        }

        private async UniTask<GameObject> Load(string assetPath)
        {
            GameObject prefab = await _assetProvider.Load<GameObject>(assetPath);
            GameObject instance = _instantiator.InstantiatePrefab(prefab);
            return instance;
        }
    }
}