using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(menuName = "Settings/EnemySettings")]
    public class EnemySettings: ScriptableObject
    {
        [SerializeField] private EnemyType _type;
        [SerializeField] private float _health;
        [SerializeField] private float _damage;

        public float Health => _health;
        public float Damage => _damage;
    }
}