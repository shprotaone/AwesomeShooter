using System;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.Pool;

namespace Infrastructure.ECS.Systems
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private BulletSettings _bulletSettings;

        private ObjectPool<Projectile> _bulletPool;
        private EcsPackedEntity _packedEntity;

        public float Damage => _bulletSettings.damage;
        public bool IsActive => gameObject.activeSelf;
        public BulletSettings Settings => _bulletSettings;
        public EcsPackedEntity PackedEntity => _packedEntity;

        public void SetPool(ObjectPool<Projectile> pool) =>
            _bulletPool = pool;

        public void SetPosition(Vector3 projectileComponentTransform) =>
            transform.position = projectileComponentTransform;

        public void DestroyBullet() => Destroy(this.gameObject);

        public void DisableBullet()
        {
            if (gameObject.activeSelf)
            {
                _bulletPool.Release(this);
            }
        }

        public void SetPackEntity(EcsPackedEntity packEntity) => _packedEntity = packEntity;
    }
}