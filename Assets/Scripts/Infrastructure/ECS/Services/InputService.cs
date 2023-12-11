using System;
using UnityEngine;
using Zenject;

namespace Infrastructure.ECS.Services
{
    public class InputService
    {
        public event Action<Vector2> OnHorizontalInput;
        public event Action OnJumpPressed;

        private PlayerControls _controls;
        private PlayerControls.GroundMovementActions _movement;
        public Vector2 HorizontalInput { get; private set; }
        private Vector2 _mouseInput;
        
        public InputService()
        {
            _movement = new PlayerControls().GroundMovement;
            _movement.Enable();
            
            SetHorizontalDirection();
            SetMouseDirection();
            SetButtonJump();
        }

        private void SetMouseDirection()
        {
            _movement.MouseX.performed += ctx =>
                _mouseInput.x = ctx.ReadValue<float>();

            _movement.MouseY.performed += ctx =>
                _mouseInput.y = ctx.ReadValue<float>();
        }

        private void SetHorizontalDirection()
        {
            _movement.Horizontal.performed += ctx =>
                OnHorizontalInput?.Invoke(ctx.ReadValue<Vector2>());
        }

        private void SetButtonJump()
        {
            _movement.Jump.performed += ctx => OnJumpPressed?.Invoke();
        }
    }
}