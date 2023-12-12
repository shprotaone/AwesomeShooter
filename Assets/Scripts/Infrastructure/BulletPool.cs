using System;
using UnityEngine.Pool;

namespace Infrastructure.ECS.Systems
{
    public class BulletPool
    {
        private ObjectPool<Bullet> _bulletPool;
        private BulletFactory _bulletFactory;
        private BulletType _bulletType;
        private Bullet _currentBullet;
        private int _defaultCapacity = 10;
        private int _maxCapacity = 100;

        public BulletPool(BulletFactory factory)
        {
            _bulletFactory = factory;
        }
        public async void Init()
        {
            _currentBullet = await _bulletFactory.GetBulletAsync(_bulletType);

            _bulletPool = new ObjectPool<Bullet>(GetBullet,
                OnTakeBulletFromPool,
                OnReleaseBulletFromPool,
                OnOverPooling,
                true,
                _defaultCapacity,
                _maxCapacity);
        }

        public Bullet Get()
        {
            return _bulletPool.Get();
        }

        public void SetBulletType(BulletType type) =>
            _bulletType = type;

        private Bullet GetBullet() => _currentBullet;

        private void OnTakeBulletFromPool(Bullet bullet) =>
            bullet.gameObject.SetActive(true);

        private void OnReleaseBulletFromPool(Bullet bullet) =>
            bullet.gameObject.SetActive(false);

        private void OnOverPooling(Bullet bullet) =>
            bullet.DestroyBullet();



    }
}