using System;
using UnityEngine;
using Zenject;

namespace Infrastructure.ECS.Services
{
    public class InputService
    {
        public event Action<Vector2> OnHorizontalInput;
        public event Action<Vector2> OnMouseInput;
        public event Action OnJumpPressed;
        public event Action OnAttackButton;
        public event Action OnReload; 

        private PlayerControls _controls;
        private PlayerControls.GroundMovementActions _movement;
        
        public InputService()
        {
            _movement = new PlayerControls().GroundMovement;
            _movement.Enable();
            
            SetHorizontalDirection();
            SetMouseDirection();
            SetButtonJump();
            SetWeaponAction();
        }

        private void SetWeaponAction()
        {
            _movement.Fire.performed += ctx => OnAttackButton?.Invoke();
            _movement.Reload.performed += ctx => OnReload?.Invoke();
        }

        private void SetMouseDirection()
        {
            _movement.Look.performed += ctx =>
                OnMouseInput?.Invoke(ctx.ReadValue<Vector2>());
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