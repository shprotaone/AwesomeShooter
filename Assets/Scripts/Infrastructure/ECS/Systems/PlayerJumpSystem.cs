using System;
using Infrastructure.ECS.Components;
using Infrastructure.ECS.Components.Providers;
using Infrastructure.ECS.Components.Tags;
using Infrastructure.ECS.Services;
using Leopotam.EcsLite;
using MonoBehaviours.Interfaces;
using Settings;
using UnityEditor;
using UnityEngine;

namespace Infrastructure.ECS.Systems
{
    sealed class PlayerJumpSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _jumpFilter;
        private EcsPool<MovableComponent> _movablePool;
        private EcsPool<GroundCheckComponent> _groundCheckPool;
        private EcsPool<GravityComponent> _gravityComponentPool;
        private PlayerSettingsSO _playerSettings;

        private InputService _inputService;

        private bool _isJumped;
        public PlayerJumpSystem(InputService inputService)
        {
            _inputService = inputService;
        }

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _playerSettings = systems.GetShared<IGameSceneData>().PlayerSettingsSo;

            _jumpFilter = _world.Filter<PlayerTag>()
                .Inc<JumpComponent>()
                .Inc<MovableComponent>().End();

            _movablePool = _world.GetPool<MovableComponent>();
            _groundCheckPool = _world.GetPool<GroundCheckComponent>();
            _gravityComponentPool = _world.GetPool<GravityComponent>();

            _inputService.OnJumpPressed += Jump;

        }

        private void Jump()
        {
            _isJumped = true;
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _jumpFilter)
            {
                ref var movableComponent = ref _movablePool.Get(entity);
                ref var groundCheckComponent = ref _groundCheckPool.Get(entity);
                ref var gravityComponent = ref _gravityComponentPool.Get(entity);

                ref var velocity = ref movableComponent.velocity;

                if(!groundCheckComponent.isGrounded) continue;

                if (_isJumped)
                {
                    velocity.y = Mathf.Sqrt(_playerSettings.JumpForce * 2f * gravityComponent.gravity);
                    _isJumped = false;
                }

            }
        }
    }
}