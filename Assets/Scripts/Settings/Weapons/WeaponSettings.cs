using Infrastructure.ECS.Components;
using UnityEngine;

namespace Settings.Weapons
{
    [CreateAssetMenu(menuName = "Settings/WeaponSettings")]
    public class WeaponSettings : ScriptableObject
    {
        public WeaponType WeaponType;
        public Vector3 positionPreset;
        public float fireRate;
        public int magazineCapacity;
        public ItemType ItemType;
    }
}