using System;

namespace Infrastructure.ECS.Components
{
    [Serializable]
    public struct EnemyCollisionComponent
    {
        public Enemy enemy;
        public CollisionEvent collision;
    }
}