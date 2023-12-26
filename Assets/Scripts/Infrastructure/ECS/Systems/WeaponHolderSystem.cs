using Infrastructure.ECS.Components;
using Infrastructure.Services;
using Leopotam.EcsLite;
using UIComponents;
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
        private EcsPool<AmmoComponent> _ammoPool;
        private EcsPool<ColliderComponent> _colliderPool;

        private UIService _uiService;
        public WeaponHolderSystem(UIService uiService)
        {
            _uiService = uiService;
        }

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _weaponHolderFilter = _world.Filter<ModelComponent>().Inc<WeaponHolderComponent>().End();
            _pickUpRequestFilter = _world.Filter<PickUpRequest>().End();

            _weaponHolderPool = _world.GetPool<WeaponHolderComponent>();
            _modelPool = _world.GetPool<ModelComponent>();
            _weaponPool = _world.GetPool<WeaponComponent>();
            _ammoPool = _world.GetPool<AmmoComponent>();
            _pickUpRequestPool = _world.GetPool<PickUpRequest>();
            _colliderPool = _world.GetPool<ColliderComponent>();
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

            ref var colliderComponent = ref _colliderPool.Get(weapon);
            colliderComponent.collider.enabled = false;

            ref var magazine = ref _ammoPool.Get(weapon);

            SetWeaponPosition(weapon,weaponComponent, weaponHolderComponent);
            SetUpView(ref magazine);
        }

        private void SetUpView(ref AmmoComponent magazine)
        {
            WeaponView view = _uiService.GetView<WeaponView>();
            magazine.OnCurrentAmmo = view.ChangeAmmoCount;
            view.ChangeAmmoCount(magazine.maxCapacity);
            view.SetMaxAmmoCount(magazine.maxCapacity);
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