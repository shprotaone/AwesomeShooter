
using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Infrastructure.ECS.Components
{
    [Serializable]
    public struct ModelComponent
    {
        public Transform modelTransform;
    }
}
