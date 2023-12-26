using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagment;
using Settings;
using UnityEngine;
using Zenject;

namespace Infrastructure.CommonSystems
{
    public class PlayerFactory : IPlayerFactory
    {
        private IAssetProvider _assetProvider;
        private IInstantiator _instantiator;

        [Inject]
        public void Construct(IAssetProvider assetProvider, IInstantiator instantiator)
        {
            _assetProvider = assetProvider;
            _instantiator = instantiator;
        }

        public async UniTask<GameObject> GetPlayer()
        {
            GameObject prefab = await _assetProvider.Load<GameObject>(AssetAddress.PlayerPrefabPath);
            return prefab;
        }

        public async UniTask<PlayerSettingsSO> GetPlayerSettings()
        {
            PlayerSettingsSO settingsSo =
                await _assetProvider.Load<PlayerSettingsSO>(AssetAddress.PlayerSettingsSOPath);
            return settingsSo;
        }

        public async UniTask<PlayerLevelSettingsSO> GetLevelsStorage()
        {
            PlayerLevelSettingsSO settingsSo =
                await _assetProvider.Load<PlayerLevelSettingsSO>(AssetAddress.PlayerLevelSettingsSOPath);
            return settingsSo;
        }
    }
}