using Infrastructure.ECS.Services;
using Infrastructure.ECS.Systems;
using Leopotam.EcsLite;
using Settings;
using UnityEngine;
using Voody.UniLeo.Lite;
using Zenject;
using PickUpSystem = Infrastructure.ECS.Components.PickUpSystem;

namespace Infrastructure.ECS
{
    sealed class EcsStartup : MonoBehaviour 
    {
        private PlayerSettingsSO _playerSettingsSo;

        private EcsWorld _world;
        private EcsSystems _systems;
        private EcsSystems _fixedUpdateSystems;
        private InputService _inputService;
        private BulletPool _bulletPool;
        private PlayerInitSystem _playerInitSystem;
        private DiContainer _container;

        [Inject]
        private void Construct(InputService inputService,
            BulletPool bulletPool,
            PlayerInitSystem playerInitSystem)
        {
            _inputService = inputService;
            _bulletPool = bulletPool;
            _playerInitSystem = playerInitSystem;
        }

        private void Start ()
        {
            _world = new EcsWorld();

            _systems = new EcsSystems(_world);
            _fixedUpdateSystems = new EcsSystems(_world);
            _systems.ConvertScene();
            AddSystems();

            _systems
#if UNITY_EDITOR
                .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ());
#endif
            _systems.Init();
            _fixedUpdateSystems.Init();
        }

        private void AddSystems()
        {
            _systems
                .Add(_playerInitSystem)
                .Add(new InputSystem(_inputService))
                .Add(new MovementSystem())
                .Add(new PlayerJumpSystem(_inputService))
                .Add(new PlayerMouseLookSystem(_inputService))
                .Add(new CursorLockSystem())
                .Add(new GravitySystem())
                .Add(new PlayerWeaponShootSystem(_inputService, _bulletPool))
                .Add(new ProjectileMovementSystem())
                .Add(new LifetimeSystem())
                .Add(new FireRateSystem())
                .Add(new ObstacleCollisionCheckSystem())
                .Add(new EnemyCollisionCheckSystem())
                .Add(new ReloadMagazineSystem(_inputService))
                .Add(new WeaponHolderSystem())
                .Add(new PickUpSystem());

            _fixedUpdateSystems.Add(new PlayerGroundCheckSystem());
        }

        private void Update ()
        {
            _systems?.Run ();
        }

        private void FixedUpdate()
        {
            _fixedUpdateSystems?.Run();
        }

        private void OnDestroy () {
            if (_systems != null) {
                _systems.Destroy ();
                _systems = null;
            }

            if (_world != null) {
                _world.Destroy ();
                _world = null;
            }
        }
    }
}