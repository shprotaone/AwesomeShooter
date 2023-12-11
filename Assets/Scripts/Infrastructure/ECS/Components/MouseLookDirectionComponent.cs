using System;
using System.Numerics;

namespace Infrastructure.ECS.Components
{
    [Serializable]
    public struct MouseLookDirectionComponent
    {
        public float sensitivityX;
        public float sensitivityY;
        public Vector3 lookDirection;
    }
}