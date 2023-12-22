using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagment;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Objects;
using Settings;
using UnityEngine;
using Zenject;

namespace Infrastructure.Factories
{
    public class EnemyFactory : IEnemyFactory
    {
        private IAssetProvider _assetProvider;
        private IInstantiator _instantiator;

        public EnemyFactory(IAssetProvider assetProvider, IInstantiator instantiator)
        {
            _assetProvider = assetProvider;
            _instantiator = instantiator;
        }

        public async UniTask<Enemy> GetEnemy(EnemyType type)
        {
            string enemyPrefabPath = "";
            if (type == EnemyType.COMMON)
            {
                enemyPrefabPath = AssetAddress.CommonEnemyPrefabPath;
            }

            GameObject enemyPrefab = await _assetProvider.Load<GameObject>(enemyPrefabPath);
            GameObject constructEnemy = _instantiator.InstantiatePrefab(enemyPrefab);

            return constructEnemy.GetComponent<Enemy>();
        }
    }
}