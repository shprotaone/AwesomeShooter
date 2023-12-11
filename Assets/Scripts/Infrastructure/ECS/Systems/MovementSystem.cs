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

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _movableFilter = _world.Filter<MovableComponent>().
                Inc<DirectionComponent>().
                Inc<ModelComponent>().End();
            
            _movableComponent = _world.GetPool<MovableComponent>();
            _directionComponent = _world.GetPool<DirectionComponent>();
            _modelComponent = _world.GetPool<ModelComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _movableFilter)
            {
                ref var movableComponent = ref _movableComponent.Get(entity);
                ref var directionComponent = ref _directionComponent.Get(entity);
                ref var modelComponent = ref _modelComponent.Get(entity);

                ref var direction = ref directionComponent.direction;
                ref var transform = ref modelComponent.modelTransform;
                
                ref var characterController = ref movableComponent.characterController;
                ref var speed = ref movableComponent.speed;
                
                var rawDirection = (transform.right * direction.x) + (transform.forward * direction.z);
                characterController.Move(rawDirection * speed * Time.deltaTime);
            }
        }
    }
}