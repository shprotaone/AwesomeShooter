using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(menuName = "Settings/PlayerSettings")]
    public class PlayerSettingsSO : ScriptableObject
    {
        [Header("Настройки передвижения")]
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _groundDistance;
        [SerializeField] private LayerMask _floorLayer;

        [Header("Настройки камеры")]
        [SerializeField] private float _camClampAngle;
        [SerializeField] private float _camHorizontalSpeed;
        [SerializeField] private float _camVerticalSpeed;

        [Header("Настройки игрока")] 
        [SerializeField] private float _health;

        public float Speed => _speed;
        public float JumpForce => _jumpForce;
        public float GroundDistance => _groundDistance;
        public LayerMask FloorLayer => _floorLayer;
        public float Health => _health;

        public float ClampAngle => _camClampAngle;
        public float HorizontalSpeed => _camHorizontalSpeed;
        public float VerticalSpeed => _camVerticalSpeed;
    }
}