using Infrastructure.ECS.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Infrastructure.ECS.Systems
{
    public class WeaponHolderSystem : IEcsInitSystem,IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _pickUpRequestFilter;
        private EcsFilter _weaponHolderFilter;

        private EcsPool<ModelComponent> _modelPool;
        private EcsPool<WeaponHolderComponent> _weaponHolderPool;
        private EcsPool<WeaponComponent> _weaponPool;
        private EcsPool<PickUpRequest> _pickUpRequestPool;
        private EcsPool<AmmoMagazineComponent> _ammoPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _weaponHolderFilter = _world.Filter<ModelComponent>().Inc<WeaponHolderComponent>().End();
            _pickUpRequestFilter = _world.Filter<PickUpRequest>().End();

            _weaponHolderPool = _world.GetPool<WeaponHolderComponent>();
            _modelPool = _world.GetPool<ModelComponent>();
            _weaponPool = _world.GetPool<WeaponComponent>();
            _ammoPool = _world.GetPool<AmmoMagazineComponent>();
            _pickUpRequestPool = _world.GetPool<PickUpRequest>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int request in _pickUpRequestFilter)
            {
                ref var requestComponent = ref _pickUpRequestPool.Get(request);

                if (requestComponent.itemType == ItemType.WEAPON)
                {
                    var weaponHolderEntity = FindHolderEntity();
                    SetUpWeaponHolder(weaponHolderEntity, requestComponent);

                    _pickUpRequestPool.Del(request);
                    _world.DelEntity(request);
                }
            }
        }

        private void SetUpWeaponHolder(int? weaponHolderEntity, PickUpRequest requestComponent)
        {
            if (weaponHolderEntity != null)
            {
                ref var weaponHolderComponent = ref _weaponHolderPool.Get((int)weaponHolderEntity);

                if (!weaponHolderComponent.IsBusy)
                {
                    weaponHolderComponent.IsBusy = true;
                    SetUpWeapon(requestComponent, weaponHolderComponent);
                }
            }
        }

        private void SetUpWeapon(PickUpRequest requestComponent,WeaponHolderComponent weaponHolderComponent)
        {
            requestComponent.entity.Unpack(_world, out int weapon);
            ref var weaponComponent = ref _weaponPool.Get(weapon);
            weaponComponent.isEquipped = true;
            SetAmmo(weapon,weaponComponent);
            SetWeaponPosition(weapon,weaponComponent, weaponHolderComponent);
        }

        private void SetAmmo(int entity, WeaponComponent weaponComponent)
        {
            ref var magazine = ref _ammoPool.Get(entity);
            magazine._maxCapacity = weaponComponent.settings.magazineCapacity;
            magazine._currentAmmo = weaponComponent.settings.magazineCapacity;

        }

        private void SetWeaponPosition(int weaponEntity, WeaponComponent weaponComponent,WeaponHolderComponent weaponHolder)
        {
            ref var weaponModelTransform = ref _modelPool.Get(weaponEntity).modelTransform;
            weaponModelTransform.SetParent(weaponHolder.holderTransform);
            weaponModelTransform.localPosition = weaponComponent.settings.positionPreset;
            weaponModelTransform.localEulerAngles = Vector3.zero;
        }

        private int? FindHolderEntity()
        {
            foreach (int holder in _weaponHolderFilter)
            {
                ref var holderBusy = ref _weaponHolderPool.Get(holder).IsBusy;

                if (!holderBusy)
                {
                    return holder;
                }
            }

            return null;
        }
    }
}