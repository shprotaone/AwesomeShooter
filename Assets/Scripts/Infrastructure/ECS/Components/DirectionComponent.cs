using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Infrastructure.ECS
{
    [Serializable]
    public struct DirectionComponent
    {
        [HideInInspector] public Vector3 direction;
    }
}