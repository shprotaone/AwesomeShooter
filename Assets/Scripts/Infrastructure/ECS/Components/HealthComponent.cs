using System;

namespace Infrastructure.ECS.Components
{
    public struct HealthComponent
    {
        public Action<float> OnHealthChanged;
        public float health;
    }
}