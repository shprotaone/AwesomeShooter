using System;
using UnityEngine;

namespace Infrastructure.ECS.Components
{
    [Serializable]
    public struct MovableComponent
    {
        public CharacterController characterController;
        public float speed;
    }
}