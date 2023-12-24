using System;

namespace Infrastructure.ECS.Components
{
    public struct DeathComponent
    {
        public Action OnDeath;
    }
}