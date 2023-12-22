using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagment;
using Infrastructure.ECS.Systems;
using Settings.Weapons;
using UnityEngine;
using Zenject;

namespace Infrastructure.Factories
{
    public class WeaponFactory : IWeaponFactory
    {
        private IInstantiator _instantiator;
        private IAssetProvider _assetProvider;

        public WeaponFactory(IAssetProvider assetProvider,IInstantiator instantiator)
        {
            _assetProvider = assetProvider;
            _instantiator = instantiator;
        }

        public async UniTask<GameObject> GetWeapon(WeaponType weaponType)
        {
            string weaponPath = "";

            if (weaponType == WeaponType.BASE)
            {
                weaponPath = AssetAddress.WeaponBaseModelPath;
            }
            GameObject result = await _assetProvider.Load<GameObject>(weaponPath);
            GameObject newObject = _instantiator.InstantiatePrefab(result);

            return newObject;
        }
    }
}