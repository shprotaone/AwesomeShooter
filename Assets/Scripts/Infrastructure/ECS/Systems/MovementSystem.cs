using Infrastructure.CommonSystems;
using Infrastructure.ECS.Components;
using Leopotam.EcsLite;
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
        private LevelSettingsContainer _levelSettings;

        public MovementSystem(LevelSettingsContainer levelSettings)
        {
            _levelSettings = levelSettings;
        }

        public async void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _movableFilter = _world.Filter<MovableComponent>().
                Inc<DirectionComponent>().
                Inc<ModelComponent>().
                Inc<GravityComponent>().End();
            
            _movableComponent = _world.GetPool<MovableComponent>();
            _directionComponent = _world.GetPool<DirectionComponent>();
            _modelComponent = _world.GetPool<ModelComponent>();
            _mouseLookComponent = _world.GetPool<MouseLookDirectionComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _movableFilter)
            {
                ref var direction = ref _directionComponent.Get(entity).direction;
                ref var camera = ref _mouseLookComponent.Get(entity).camera;
                ref var characterController = ref _movableComponent.Get(entity).characterController;
                ref var velocity = ref _movableComponent.Get(entity).velocity;
                ref var modelComponent = ref _modelComponent.Get(entity);

                var cameraTransform = camera.transform;

                var rawDirection = (cameraTransform.right * direction.x) +
                                   (cameraTransform.forward * direction.z);

                rawDirection.y = velocity.y;
                modelComponent.modelTransform.rotation = cameraTransform.rotation;
                characterController.Move
                    (rawDirection * _levelSettings.PlayerSettings.Speed * Time.deltaTime);
            }
        }
    }
}