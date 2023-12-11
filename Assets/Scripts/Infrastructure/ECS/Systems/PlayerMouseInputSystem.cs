using System;
using Infrastructure.ECS.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Infrastructure.ECS.Systems
{
    sealed class PlayerMouseInputSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _playerFilter;
        private EcsPool<MouseLookDirectionComponent> _lookDirectionComponent;
        private PlayerControls.GroundMovementActions _movement;
        //TODO : Переделать на InputService

        private Vector2 _lookDirection;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _playerFilter = _world.Filter<MouseLookDirectionComponent>().End();

            _lookDirectionComponent = _world.GetPool<MouseLookDirectionComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _playerFilter)
            {
                
            }
        }
    }
}