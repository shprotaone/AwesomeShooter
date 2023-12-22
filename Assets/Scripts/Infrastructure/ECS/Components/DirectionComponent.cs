using System;
using UnityEngine;

namespace Infrastructure.ECS.Components
{
    [Serializable]
    public struct DirectionComponent
    {
        [HideInInspector] public Vector3 direction;
    }
}