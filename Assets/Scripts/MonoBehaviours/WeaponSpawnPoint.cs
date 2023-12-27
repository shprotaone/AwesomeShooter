using System;
using Settings.Weapons;
using Unity.VisualScripting;
using UnityEngine;

namespace MonoBehaviours
{
    public class WeaponSpawnPoint : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private WeaponType _weaponType;
        [SerializeField] private bool _isPicked;

        public Transform Transform => _transform;

        public WeaponType WeaponType => _weaponType;

        public bool IsPicked => _isPicked;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position,1);
        }
    }
}