using System;
using Leopotam.EcsLite;

namespace Infrastructure.ECS.Components
{
    [Serializable]
    public struct ObstacleCollisionComponent
    {
        public CollisionEvent collision;
    }
}