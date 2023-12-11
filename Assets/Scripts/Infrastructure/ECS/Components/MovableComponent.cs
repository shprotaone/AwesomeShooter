using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Infrastructure.ECS.Components
{
    [Serializable]
    public struct MovableComponent
    {
        public CharacterController characterController;
        public float speed;
    }
}