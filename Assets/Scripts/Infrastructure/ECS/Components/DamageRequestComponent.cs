using Leopotam.EcsLite;
using UnityEngine.Serialization;

namespace Infrastructure.ECS.Components
{
    public struct DamageRequestComponent
    {
        public EcsPackedEntity packEntity;
        public float damage;
    }
}