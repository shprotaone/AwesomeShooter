using System;
using UnityEngine;

namespace Infrastructure.ECS.Components
{
    [Serializable]
    public struct MovableComponent
    {
        public CharacterController characterController;
        [HideInInspector] public Vector3 velocity;
        [HideInInspector] public float speed;
    }
}