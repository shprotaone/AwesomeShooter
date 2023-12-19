using System;
using Infrastructure.ECS.Components;
using Infrastructure.ECS.Services;
using Leopotam.EcsLite;
using Settings;
using UnityEditor;
using UnityEngine;

namespace Infrastructure.ECS.Systems
{
    sealed class PlayerMouseLookSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly InputService _inputService;

        private EcsWorld _world;
        private EcsFilter _playerFilter;
        private EcsPool<MouseLookDirectionComponent> _lookDirectionComponent;
        private PlayerSettingsSO _playerSettings;

        private Vector2 _deltaInput;
        private Vector3 _startTransformRotation;

        public PlayerMouseLookSystem(InputService inputService)
        {
            _inputService = inputService;
            _inputService.OnMouseInput += SetDelta;
        }

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _playerSettings = systems.GetShared<PlayerSettingsSO>();

            _playerFilter = _world.Filter<MouseLookDirectionComponent>().
                Inc<ModelComponent>().
                End();

            _lookDirectionComponent = _world.GetPool<MouseLookDirectionComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _playerFilter)
            {
                ref var lookCameraComponent = ref _lookDirectionComponent.Get(entity);

                _startTransformRotation.x +=
                    _deltaInput.x * _playerSettings.VerticalSpeed * Time.deltaTime;

                _startTransformRotation.y +=
                    _deltaInput.y * _playerSettings.HorizontalSpeed * Time.deltaTime;

                _startTransformRotation.y =
                    Math.Clamp(_startTransformRotation.y, -_playerSettings.ClampAngle,
                        _playerSettings.ClampAngle);

                lookCameraComponent.camExtension.SetRotation(_startTransformRotation);
            }
        }

        private void SetDelta(Vector2 delta)
        {
            _deltaInput = delta;
        }
    }
}