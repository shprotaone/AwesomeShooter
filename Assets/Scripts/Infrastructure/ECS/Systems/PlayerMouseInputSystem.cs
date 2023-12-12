using System;
using Infrastructure.ECS.Components;
using Infrastructure.ECS.Services;
using Leopotam.EcsLite;
using UnityEngine;

namespace Infrastructure.ECS.Systems
{
    sealed class PlayerMouseInputSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _playerFilter;
        private EcsPool<MouseLookDirectionComponent> _lookDirectionComponent;
        private EcsPool<ModelComponent> _modelComponent;
        private Quaternion _startTransformRotation;
        private InputService _inputService;

        private Vector2 _lookDirection;

        public PlayerMouseInputSystem(InputService inputService)
        {
            _inputService = inputService;
        }

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _playerFilter = _world.Filter<MouseLookDirectionComponent>().
                Inc<ModelComponent>().
                End();

            _lookDirectionComponent = _world.GetPool<MouseLookDirectionComponent>();
            _modelComponent = _world.GetPool<ModelComponent>();

            GetStartRotation();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _playerFilter)
            {
                ref var modelComponent = ref _modelComponent.Get(entity);
                ref var lookCameraComponent = ref _lookDirectionComponent.Get(entity);
                
                
            }
        }

        private void GetStartRotation()
        {
            foreach (int entity in _playerFilter)
            {
                ref var modelComponent = ref _modelComponent.Get(entity);
                _startTransformRotation = modelComponent.modelTransform.rotation;
            }
        }
    }
}