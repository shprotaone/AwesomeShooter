using Infrastructure.ECS.Components;
using Infrastructure.ECS.Components.Tags;
using Infrastructure.ECS.Services;
using Leopotam.EcsLite;
using UnityEngine;

namespace Infrastructure.ECS.Systems
{
    sealed class InputSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _inputFilter;
        private EcsPool<DirectionComponent> _directions;
        private InputService _inputService;

        private Vector2 _horizontalInput;

        public InputSystem(InputService inputService)
        {
            _inputService = inputService;
        }

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _inputService.OnHorizontalInput += SetDirection;

            _inputFilter = _world.Filter<PlayerTag>().Inc<DirectionComponent>().End();
            _directions = _world.GetPool<DirectionComponent>();
        }

        private void SetDirection(Vector2 direction) => _horizontalInput = direction;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _inputFilter)
            {
                ref DirectionComponent directionComponent = ref _directions.Get(entity);
                ref var direction = ref directionComponent.direction;

                direction.x = _horizontalInput.x;
                direction.z = _horizontalInput.y;
            }
        }
    }
}