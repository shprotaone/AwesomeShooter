using System;
using UnityEngine;

namespace Infrastructure.ECS.Systems
{
    [Serializable]
    public struct WeaponHolderComponent
    {
        public bool IsBusy;
        public Transform holderTransform;
    }
}