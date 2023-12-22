using System;
using Leopotam.EcsLite;
using Settings;
using UnityEngine;
using UnityEngine.Pool;

namespace Objects
{
    public class Enemy : MonoBehaviour
    {
        public event Action OnDeath;

        [SerializeField] private EnemySettings _settings;
        [SerializeField] private float health;

        private ObjectPool<Enemy> _pool;
        private EcsPackedEntity _packedEntity;

        public EcsPackedEntity PackedEntity => _packedEntity;
        public EnemySettings EnemySettings => _settings;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            health = _settings.Health;
        }

        public void SetPool(ObjectPool<Enemy> pool) =>
            _pool = pool;

        public void DestroyObj()
        {
            Destroy(this.gameObject);
        }

        public void SetPackedEntity(EcsPackedEntity packEntity)
        {
            _packedEntity = packEntity;
        }
    }
}