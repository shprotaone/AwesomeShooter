using Settings;
using UnityEngine;

namespace MonoBehaviours
{
    public class EnemySpawnPoint : MonoBehaviour
    {
        [SerializeField] private GameObject _enable;
        [SerializeField] private bool _isActive;
        [SerializeField] private Transform _transform;
        [SerializeField] private EnemyType _enemyType;

        private Color _gizmosColor = Color.red;

        public Transform Transform => _transform;
        public bool IsActive => _isActive;
        public EnemyType EnemyType => _enemyType;

        public bool IsAlive => gameObject.activeSelf;

        public void SetWorks()
        {
            _isActive = true;
            _enable.SetActive(true);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = _gizmosColor;
            Gizmos.DrawSphere(transform.position, 1);
        }

        public void Disable()
        {
            _isActive = false;
            _enable.SetActive(false);
        }
    }
}