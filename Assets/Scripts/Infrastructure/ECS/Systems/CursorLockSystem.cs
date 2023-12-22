using Leopotam.EcsLite;
using UnityEngine;

namespace Infrastructure.ECS.Systems
{
    public class CursorLockSystem : IEcsInitSystem
    {
        public void Init(IEcsSystems systems)
        {
            //Cursor.lockState = CursorLockMode.Locked;
        }
    }
}