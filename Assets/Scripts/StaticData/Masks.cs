using UnityEngine;

namespace Infrastructure.ECS.Components
{
    public static class Masks
    {
        public static LayerMask Pickable = LayerMask.GetMask("Pickable");
        public static LayerMask Floor = LayerMask.GetMask("Floor");
        public static LayerMask Enemy = LayerMask.GetMask("Enemy");
    }
}