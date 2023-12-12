using System;
using Infrastructure.ECS.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Infrastructure.ECS.Systems
{
    sealed class PlayerGroundCheckSystem : IEcsInitSystem,IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _groundFilter;
        private EcsPool<GroundCheckComponent> _groundPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _groundFilter = _world.Filter<GroundCheckComponent>().End();
            _groundPool = _world.GetPool<GroundCheckComponent>();

        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _groundFilter)
            {
                ref var groundCheck = ref _groundPool.Get(entity);

                groundCheck.isGrounded = Physics.CheckSphere(groundCheck.groundCheckTransform.position,
                    groundCheck.groundDistance, groundCheck.groundMask);
            }
        }
    }
}