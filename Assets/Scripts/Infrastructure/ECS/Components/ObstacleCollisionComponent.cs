using System;
using Leopotam.EcsLite;
using Objects;

namespace Infrastructure.ECS.Components
{
    [Serializable]
    public struct ObstacleCollisionComponent
    {
        public CollisionEvent collision;
    }
}