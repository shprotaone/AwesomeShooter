using System;
using Objects;
using UnityEngine.Serialization;

namespace Infrastructure.ECS.Components
{
    [Serializable]
    public struct EnemyCollisionComponent
    {
        public Enemy enemy;
        public OnTriggerEvent onTrigger;
    }
}