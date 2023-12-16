using Leopotam.EcsLite;
using UnityEngine;

namespace Infrastructure.ECS.Components
{
    public struct PickUpRequest
    {
        public ItemType itemType;
        public EcsPackedEntity entity;
    }
}