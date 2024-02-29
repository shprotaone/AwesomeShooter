using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(menuName = "Settings/EnemySettings")]
    public class EnemySettings: ScriptableObject
    {
        [SerializeField] private EnemyType _type;
        [SerializeField] private float _health;
        [SerializeField] private float _damage;
        [SerializeField] private float _speed;
        [SerializeField] private float _damageTime;
        [SerializeField] private float _damageDistance;

        public float Health => _health;
        public float Damage => _damage;
        public float Speed => _speed;
        public float DamageTime => _damageTime;
        public float DamageDistance => _damageDistance;
    }
}