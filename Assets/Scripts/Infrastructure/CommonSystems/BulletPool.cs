using Infrastructure.Factories;
using Objects;
using UnityEngine;
using UnityEngine.Pool;

namespace Infrastructure
{
    public class BulletPool
    {
        private ObjectPool<Projectile> _bulletPool;
        private BulletFactory _bulletFactory;
        private BulletType _bulletType;
        private GameObject _bulletPrefab;
        private int _defaultCapacity = 10;
        private int _maxCapacity = 100;

        public ObjectPool<Projectile> Pool => _bulletPool;

        public BulletPool(BulletFactory factory)
        {
            _bulletFactory = factory;
        }

        public async void Init()
        {
            _bulletPrefab = await _bulletFactory.GetBulletAsync(_bulletType);

            _bulletPool = new ObjectPool<Projectile>(CreateBullet,
                OnTakeBulletFromPool,
                OnReleaseBulletFromPool,
                OnOverPooling,
                true,
                _defaultCapacity,
                _maxCapacity);
        }

        public void SetBulletType(BulletType type) =>
            _bulletType = type;

        private Projectile CreateBullet()
        {
            Projectile projectile = GameObject.Instantiate(_bulletPrefab).GetComponent<Projectile>();
            projectile.SetPool(_bulletPool);
            return projectile;
        }


        private void OnTakeBulletFromPool(Projectile projectile) =>
            projectile.gameObject.SetActive(true);

        private void OnReleaseBulletFromPool(Projectile projectile) =>
            projectile.gameObject.SetActive(false);

        private void OnOverPooling(Projectile projectile) =>
            projectile.DestroyBullet();
    }
}