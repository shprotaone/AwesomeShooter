using System;

namespace Infrastructure.ECS.Components
{
    [Serializable]
    public struct AmmoMagazineComponent
    {
        public int _maxCapacity;
        public int _currentAmmo;
    }
}