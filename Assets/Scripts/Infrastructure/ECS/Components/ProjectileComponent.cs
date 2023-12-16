using Infrastructure.ECS.Systems;
using UnityEngine;
using UnityEngine.Serialization;

namespace Infrastructure.ECS.Components
{
    [SerializeField]
    public struct ProjectileComponent
    {
        public Projectile projectile;
        public Vector3 position;
        public float speed;
    }
}