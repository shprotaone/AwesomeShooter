using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Infrastructure.Factories;
using Objects;
using Settings;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace Infrastructure.CommonSystems
{
    public class EnemyPool
    {
        private ObjectPool<Enemy> _pool;
        private IEnemyFactory _factory;
        private IInstantiator _instantiator;
        private Enemy _enemyPrefab;
        private int _defaultCapacity = 10;
        private int _maxCapacity = 100;

        public ObjectPool<Enemy> Pool => _pool;

        public EnemyPool(IEnemyFactory factory,IInstantiator instantiator)
        {
            _factory = factory;
            _instantiator = instantiator;
        }

        public async UniTask Init()
        {
            //TODO: будет несколько пулов для разных типов противника
            _enemyPrefab = await _factory.GetEnemy(EnemyType.COMMON);

            _pool = new ObjectPool<Enemy>(Create,
                TakeFromPool,
                Release,
                Destroy,
                true,
                _defaultCapacity,
                _maxCapacity);

        }

        private Enemy Create()
        {
            Enemy enemy = _instantiator
                .InstantiatePrefab(_enemyPrefab.gameObject)
                .GetComponent<Enemy>();
            enemy.SetPool(_pool);
            return enemy;
        }

        public void TakeFromPool(Enemy obj)
        {
            obj.gameObject.SetActive(true);
        }

        public void Release(Enemy obj)
        {
            obj.gameObject.SetActive(false);
        }

        public void Destroy(Enemy obj)
        {
            obj.DestroyObj();
        }
    }
}