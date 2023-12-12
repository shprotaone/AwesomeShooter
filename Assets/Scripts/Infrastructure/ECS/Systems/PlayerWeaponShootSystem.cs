using Infrastructure.ECS.Components;
using Infrastructure.ECS.Services;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.Pool;

namespace Infrastructure.ECS.Systems
{
    sealed class PlayerWeaponShootSystem : IEcsInitSystem,IEcsRunSystem,IEcsDestroySystem
    {
        private EcsFilter _playerWeaponFilter;
        private EcsPool<WeaponComponent> _playerWeaponPool;

        private BulletPool _bulletPool;
        private InputService _inputService;
        private bool _isAttack;

        public PlayerWeaponShootSystem(InputService inputService,BulletPool bulletPool)
        {
            _inputService = inputService;
            _bulletPool = bulletPool;
            _inputService.OnAttackButton += Attack;
            _bulletPool.Init(); //TODO : ASYNC TO COROUTINERUNNER?
            _bulletPool.SetBulletType(BulletType.COMMON);
            //TODO Lifetime Component
        }

        private void Attack()
        {
            _isAttack = true;
        }

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            _playerWeaponFilter = world.Filter<WeaponComponent>().Inc<WeaponPlayerTag>().End();
            _playerWeaponPool = world.GetPool<WeaponComponent>();

        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _playerWeaponFilter)
            {
                if (_isAttack)
                {
                    ref var weaponComponent = ref _playerWeaponPool.Get(entity);
                    Bullet bullet = _bulletPool.Get();
                    bullet.transform.position = weaponComponent.muzzle.position;
                    Debug.Log("Shoot");
                    _isAttack = false;
                }
            }
        }

        public void Destroy(IEcsSystems systems)
        {
            _inputService.OnAttackButton -= Attack;
        }
    }
}