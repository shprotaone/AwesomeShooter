using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagment;
using UnityEngine;
using Zenject;

namespace Infrastructure.Factories
{
    public class BulletFactory
    {
        private IInstantiator _instantiator;
        private IAssetProvider _assetProvider;

        [Inject]
        private void Construct(IAssetProvider assetProvider,IInstantiator instantiator)
        {
            _assetProvider = assetProvider;
            _instantiator = instantiator;

        }

        public async UniTask<GameObject> GetBulletAsync(BulletType type)
        {
            string address = "";
            if (type == BulletType.COMMON)
                address = AssetAddress.CommonBulletPrefabPath;

            GameObject prefab = await _assetProvider.Load<GameObject>(address);
            GameObject newObj = _instantiator.InstantiatePrefab(prefab);
            return newObj;
        }
    }
}