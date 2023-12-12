using System;
using System.Numerics;
using Cinemachine;

namespace Infrastructure.ECS.Components
{
    [Serializable]
    public struct MouseLookDirectionComponent
    {
        public CinemachineVirtualCamera camera;
        public float sensitivityX;
        public float sensitivityY;
        public Vector3 lookDirection;
    }
}