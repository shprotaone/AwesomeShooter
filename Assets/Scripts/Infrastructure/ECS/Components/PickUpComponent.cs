using System;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.Serialization;

namespace Infrastructure.ECS.Components
{
    [Serializable]
    public struct PickUpComponent
    {
        public EcsPackedEntity entity;
        public bool isPicked;
    }
}