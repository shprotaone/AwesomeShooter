using Leopotam.EcsLite;

namespace Infrastructure.ECS.Systems
{
    public struct PeriodicDamageRequestComponent
    {
        public EcsPackedEntity PackedEntity;
        public float Damage;
        public float TimeOfDamage;
    }
}