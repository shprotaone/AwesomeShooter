using Infrastructure.Factories;
using UnityEngine;

namespace Settings.Weapons
{
    [CreateAssetMenu(menuName = "Settings/BulletSettings")]
    public class BulletSettings : ScriptableObject
    {
        public BulletType bulletType;
        public float projectileSpeed;
        public float damage;
        public float lifetime;
    }
}
