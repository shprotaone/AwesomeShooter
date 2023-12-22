using Settings.Weapons;
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
    }
}