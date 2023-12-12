using UnityEngine;

namespace Infrastructure.ECS.Components
{
    [SerializeField]
    public struct ProjectileComponent
    {
        public Transform transform;
        public float speed;
    }
}