using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagment;
using MonoBehaviours.Interfaces;
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
            GameObject newObj = _instantiator.InstantiatePrefab(prefab);
            return newObj;
        }

        public async UniTask<ILevelData> LoadLevel()
        {
            GameObject prefab = await _assetProvider.Load<GameObject>(AssetAddress.FirstLevelGameSceneDataPath);
            GameObject newObj = _instantiator.InstantiatePrefab(prefab);


            return newObj.GetComponent<ILevelData>();
        }

        public async UniTask<PlayerSettingsSO> GetPlayerSettings()
        {
            PlayerSettingsSO settingsSo =
                await _assetProvider.Load<PlayerSettingsSO>(AssetAddress.PlayerSettingsSOPath);
            return settingsSo;
        }

        public async UniTask<PlayerLevelProgress> GetLevelsStorage()
        {
            PlayerLevelProgress progress =
                await _assetProvider.Load<PlayerLevelProgress>(AssetAddress.PlayerLevelSettingsSOPath);
            return progress;
        }
    }
}