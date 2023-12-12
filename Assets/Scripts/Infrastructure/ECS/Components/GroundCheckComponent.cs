using System;
using UnityEngine;

namespace Infrastructure.ECS.Components
{
    [Serializable]
    public struct GroundCheckComponent
    {
        public LayerMask groundMask;
        public Transform groundCheckTransform;
        public float groundDistance;
        public bool isGrounded;
    }
}