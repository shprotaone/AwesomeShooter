using System;
using System.Collections.Generic;
using Extention;
using Infrastructure.ECS.Components;
using Infrastructure.ECS.Components.Tags;
using Infrastructure.ECS.Services;
using Infrastructure.Factories;
using Leopotam.EcsLite;
using Objects;

namespace Infrastructure.ECS.Systems
{
    sealed class PlayerWeaponShootSystem : IEcsInitSystem,IEcsRunSystem,IEcsDestroySystem
    {
        private EcsWorld _world;
        private EcsFilter _playerWeaponFilter;
        private EcsPool<WeaponComponent> _playerWeaponPool;
        private EcsPool<FireRateComponent> _fireRatePool;
        private EcsPool<AmmoMagazineComponent> _magazinePool;

        private BulletPool _bulletPool;
        private InputService _inputService;
        private bool _isAttack;
        private float fireTimer = 0;

        public PlayerWeaponShootSystem(InputService inputService,BulletPool bulletPool)
        {
            _inputService = inputService;
            _bulletPool = bulletPool;
            _inputService.OnAttackButtonPressed += Attack;
            _bulletPool.Init();
            _bulletPool.SetBulletType(BulletType.COMMON);
        }

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _playerWeaponFilter = _world.Filter<WeaponComponent>()
                .Inc<WeaponPlayerTag>()
                .Inc<FireRateComponent>()
                .End();

            _playerWeaponPool = _world.GetPool<WeaponComponent>();
            _fireRatePool = _world.GetPool<FireRateComponent>();
            _magazinePool = _world.GetPool<AmmoMagazineComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _playerWeaponFilter)
            {
                ref var firerate = ref _fireRatePool.Get(entity).firerate;
                ref var weaponComponent = ref _playerWeaponPool.Get(entity);
                ref var magazine = ref _magazinePool.Get(entity);

                if (_isAttack && weaponComponent.isEquipped)
                {
                    if (firerate >= weaponComponent.settings.fireRate)
                    {
                        SpawnBullet(weaponComponent);
                        magazine._currentAmmo--;
                        firerate = 0;
                    }
                }
            }
        }

        private void SpawnBullet(WeaponComponent weaponComponent)
        {
            Projectile projectile = _bulletPool.Pool.Get();

            var projectileEntity = _world.NewEntity();
            projectileEntity.ConstructEntity(_world,ConstructProjectile(weaponComponent,projectile));

            projectile.SetPackEntity(_world.PackEntity(projectileEntity));

        }

        private void Attack(bool flag)
        {
            _isAttack = flag;
        }

        private List<object> ConstructProjectile(WeaponComponent weaponComponent, Projectile bul)
        {
            List<object> components = new List<object>();

            components.Add(new ProjectileComponent()
            {
                position = weaponComponent.muzzle.position,
                projectile = bul,
                speed = bul.Settings.projectileSpeed
            });

            components.Add(new LifeTimeComponent()
            {
                lifeTime = bul.Settings.lifetime
            });

            bul.transform.rotation = weaponComponent.muzzle.parent.rotation;
            return components;
        }

        public void Destroy(IEcsSystems systems)
        {
            _inputService.OnAttackButtonPressed -= Attack;
        }
    }
}