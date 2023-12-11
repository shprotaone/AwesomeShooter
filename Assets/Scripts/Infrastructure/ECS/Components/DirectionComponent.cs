using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Infrastructure.ECS
{
    [Serializable]
    public struct DirectionComponent
    {
        public Vector3 direction;
    }
}