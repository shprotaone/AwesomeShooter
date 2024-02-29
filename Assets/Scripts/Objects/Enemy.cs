using System;
using Leopotam.EcsLite;
using Settings;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

namespace Objects
{
    public class Enemy : MonoBehaviour
    {
        public event Action OnDeath;

        [SerializeField] private EnemySettings _settings;
        [SerializeField] private float _health;

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
            _health = _settings.Health;
        }

        public void SetEntityNumber(string number)
        {

        }

        public void SetPool(ObjectPool<Enemy> pool) =>
            _pool = pool;

        public void DisableObj()
        {
            _pool.Release(this);
        }

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