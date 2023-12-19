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
        private EcsFilter _modelFilter;

        private EcsPool<ModelComponent> _modelPool;
        private EcsPool<WeaponComponent> _weaponPool;
        private EcsPool<PickUpRequest> _pickUpRequestPool;
        private EcsPool<AmmoMagazineComponent> _ammoPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _modelFilter = _world.Filter<ModelComponent>().Inc<WeaponComponent>().End();
            _weaponHolderFilter = _world.Filter<ModelComponent>().Inc<WeaponHolderTag>().End();
            _pickUpRequestFilter = _world.Filter<PickUpRequest>().End();

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
                    var weaponHolder = FindHolder();
                    SetUpWeapon(requestComponent,weaponHolder);
                    _pickUpRequestPool.Del(request);
                    _world.DelEntity(request);
                }
            }
        }

        private void SetUpWeapon(PickUpRequest requestComponent,ModelComponent weaponHolderComponent)
        {
            requestComponent.entity.Unpack(_world, out int weapon);
            ref var currentWeapon = ref _weaponPool.Get(weapon);
            currentWeapon.isEquipped = true;
            SetAmmo(weapon,currentWeapon);
            SetWeaponPosition(weaponHolderComponent,currentWeapon.settings.positionPreset);
        }

        private void SetAmmo(int entity, WeaponComponent weaponComponent)
        {
            ref var magazine = ref _ammoPool.Get(entity);
            magazine._maxCapacity = weaponComponent.settings.magazineCapacity;
            magazine._currentAmmo = weaponComponent.settings.magazineCapacity;

        }

        private void SetWeaponPosition(ModelComponent weaponHolder,Vector3 presetPosition)
        {
            foreach (int weaponModel in _modelFilter)
            {
                ref var weaponModelTransform = ref _modelPool.Get(weaponModel).modelTransform;
                weaponModelTransform.SetParent(weaponHolder.modelTransform);
                weaponModelTransform.localPosition = presetPosition;
                weaponModelTransform.localEulerAngles = Vector3.zero;
            }
        }

        private ModelComponent FindHolder()
        {
            foreach (int holder in _weaponHolderFilter)
                return _modelPool.Get(holder);

            return default;
        }
    }
}