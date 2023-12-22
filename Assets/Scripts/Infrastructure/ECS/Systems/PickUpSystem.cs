using System.Collections.Generic;
using System.Linq;
using Extention;
using Infrastructure.ECS.Components;
using Infrastructure.ECS.Components.Tags;
using Infrastructure.ECS.Views;
using Leopotam.EcsLite;
using TMPro;
using UnityEngine;

namespace Infrastructure.ECS.Systems
{
    public class PickUpSystem : IEcsInitSystem,IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _pickUpFilter;
        private EcsFilter _playerFilter;
        private EcsPool<PickUpComponent> _pickUpPool;
        private EcsPool<ModelComponent> _playerPool;
        private EcsPool<WeaponHolderComponent> _weaponHolderPool;

        private EcsPool<AroundScanComponent> _aroundScanPool;
        private EcsFilter _aroundFilter;

        private Vector3 _playerPosition;
        private LayerMask _mask;
        private Collider[] _hitColliders = new Collider[1];

        public void Init(IEcsSystems systems)
        {
            _playerPosition = new Vector3();
            _mask = Masks.Pickable;
            _world = systems.GetWorld();
            _aroundFilter = _world.Filter<AroundScanComponent>().Inc<PlayerTag>().End();
            _aroundScanPool = _world.GetPool<AroundScanComponent>();

            _pickUpPool = _world.GetPool<PickUpComponent>();
            _pickUpFilter = _world.Filter<PickUpComponent>().End();

            _playerFilter = _world.Filter<ModelComponent>().Inc<PlayerTag>().End();
            _playerPool = _world.GetPool<ModelComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            UpdateOverlapPosition();
            CheckTriggers();
        }

        private void UpdateOverlapPosition()
        {
            foreach (int player in _playerFilter)
            {
                ref var playerPos = ref _playerPool.Get(player).modelTransform;
                _playerPosition = playerPos.position;
            }
        }

        private void CheckTriggers()
        {
            int numColliders = Physics.OverlapSphereNonAlloc(_playerPosition, 3, _hitColliders, _mask.value);

            foreach (int entity in _aroundFilter)
            {
                ref var aroundComponents = ref _aroundScanPool.Get(entity).around;
                if (numColliders > 0)
                {
                    aroundComponents = _hitColliders[0].name;
                    if (_hitColliders[0].TryGetComponent(out WeaponConfig weaponConfig))
                    {
                        weaponConfig.entity.Unpack(_world, out int result);
                        OnTriggerEnter(result);
                    }

                }
                else
                {
                    aroundComponents = "";
                }

            }
        }

        private void OnTriggerEnter(int entity)
        {
            int itemEntity = entity;
            ItemType type = itemEntity.GetECSComponent<ItemTypeComponent>(_world).type;
            EcsPackedEntity itemPackEntity = _world.PackEntity(itemEntity);
            int requestEntity = _world.NewEntity();
            _world.AddComponentToEntity(requestEntity,new PickUpRequest()
            {
                entity = itemPackEntity,
                itemType = type
            });
        }

        private void OnTriggerExit(Collider other)
        {
            if (((1 << other.gameObject.layer) & _mask) != 0)
            {
                Debug.Log("Enter " + other.gameObject);
            }
        }
    }
}