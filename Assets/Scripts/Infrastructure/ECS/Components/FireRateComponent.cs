using System;
using UnityEngine;

namespace Infrastructure.ECS.Components
{
    [Serializable]
    public struct FireRateComponent
    {
        [HideInInspector]public float firerate;
    }
}