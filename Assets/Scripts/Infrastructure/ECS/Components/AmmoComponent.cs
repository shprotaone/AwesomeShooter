using System;
using UnityEngine.Serialization;

namespace Infrastructure.ECS.Components
{
    [Serializable]
    public struct AmmoComponent
    {
        public Action<int> OnCurrentAmmo;
        public int maxCapacity;
        public int currentAmmo;
    }
}