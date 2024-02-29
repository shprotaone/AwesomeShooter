using System;
using Leopotam.EcsLite;
using Objects;
using UnityEngine.Serialization;

namespace Infrastructure.ECS.Components
{
    [Serializable]
    public struct ObstacleCollisionComponent
    {
        [FormerlySerializedAs("collision")] public OnTriggerEvent _onTrigger;
    }
}