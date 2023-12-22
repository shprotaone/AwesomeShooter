using Settings;
using Settings.Weapons;
using UnityEngine;

namespace MonoBehaviours
{
    public class EnemySpawnPoint : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private EnemyType _enemyType;

        public Transform Transform => _transform;

        public EnemyType EnemyType => _enemyType;

        public bool IsAlive => gameObject.activeSelf;
    }
}