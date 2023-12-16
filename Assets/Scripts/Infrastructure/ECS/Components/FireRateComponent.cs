using System;
using UnityEngine;

namespace Infrastructure.ECS.Systems
{
    [Serializable]
    public struct FireRateComponent
    {
        [HideInInspector]public float firerate;
    }
}