using System;
using Infrasructure.Settings;
using Infrastructure.ECS.Components;
using Infrastructure.ECS.Components.Providers;
using Leopotam.EcsLite;
using UnityEditor;
using UnityEngine;

namespace Infrastructure.ECS.Systems
{
    sealed class MovementSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _movableFilter;
        private EcsPool<MovableComponent> _movableComponent;
        private EcsPool<DirectionComponent> _directionComponent;
        private EcsPool<ModelComponent> _modelComponent;
        private EcsPool<MouseLookDirectionComponent> _mouseLookComponent;
        private EcsPool<GravityComponent> _gravityComponent;
        private PlayerSettingsSO _playerSettings;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _playerSettings = systems.GetShared<PlayerSettingsSO>();

            _movableFilter = _world.Filter<MovableComponent>().
                Inc<DirectionComponent>().
                Inc<ModelComponent>().
                Inc<GravityComponent>().End();
            
            _movableComponent = _world.GetPool<MovableComponent>();
            _directionComponent = _world.GetPool<DirectionComponent>();
            _modelComponent = _world.GetPool<ModelComponent>();
            _mouseLookComponent = _world.GetPool<MouseLookDirectionComponent>();
            _gravityComponent = _world.GetPool<GravityComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _movableFilter)
            {
                ref var movableComponent = ref _movableComponent.Get(entity);
                ref var directionComponent = ref _directionComponent.Get(entity);
                ref var modelComponent = ref _modelComponent.Get(entity);
                ref var mouseLookComponent = ref _mouseLookComponent.Get(entity);
                ref var gravityComponent = ref _gravityComponent.Get(entity);

                ref var direction = ref directionComponent.direction;
                ref var characterController = ref movableComponent.characterController;
                ref var camera = ref mouseLookComponent.camera;

                var cameraTransform = camera.transform;

                var rawDirection = (cameraTransform.right * direction.x) +
                                   (cameraTransform.forward * direction.z);
                ref var velocity = ref movableComponent.velocity;

                rawDirection.y = velocity.y;
                modelComponent.modelTransform.rotation = cameraTransform.rotation;
                characterController.Move(rawDirection * _playerSettings.Speed * Time.deltaTime);
            }
        }
    }
}