﻿using System;
using UnityEngine;

namespace Infrastructure.ECS.Components
{
    [Serializable]
    public struct MouseLookDirectionComponent
    {
        public Camera camera;
        public CinemachinePOVExtension camExtension;
    }
}