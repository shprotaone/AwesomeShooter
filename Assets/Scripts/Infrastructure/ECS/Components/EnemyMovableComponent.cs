using UnityEngine.AI;

namespace Infrastructure.ECS.Components
{
    public struct EnemyMovableComponent
    {
        public NavMeshAgent agent;
        public float speed;
    }
}