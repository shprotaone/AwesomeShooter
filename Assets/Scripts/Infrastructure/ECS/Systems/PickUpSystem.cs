using System.Collections.Generic;
using System.Linq;
using Extention;
using Infrastructure.ECS.Components;
using Infrastructure.ECS.Components.Tags;
using Infrastructure.ECS.Views;
using Leopotam.EcsLite;
using Objects;
using TMPro;
using UnityEngine;

namespace Infrastructure.ECS.Systems
{
    public class PickUpSystem : IEcsInitSystem,IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _playerFilter;
        private EcsFilter _aroundFilter;

        private EcsPool<DamageComponent> _damagePool;
        private EcsPool<ModelComponent> _playerPool;
        private EcsPool<WeaponHolderComponent> _weaponHolderPool;
        private EcsPool<AroundScanComponent> _aroundScanPool;

        private Vector3 _playerPosition;
        private LayerMask _mask;
        private LayerMask _enemyMask;
        private Collider[] _hitColliders = new Collider[1];

        public void Init(IEcsSystems systems)
        {
            _playerPosition = new Vector3();
            _mask = Masks.Pickable;
            _enemyMask = Masks.Enemy;
            _world = systems.GetWorld();
            _aroundFilter = _world.Filter<AroundScanComponent>().Inc<PlayerTag>().End();
            _aroundScanPool = _world.GetPool<AroundScanComponent>();

            _playerFilter = _world.Filter<ModelComponent>().Inc<PlayerTag>().End();
            _playerPool = _world.GetPool<ModelComponent>();

            _damagePool = _world.GetPool<DamageComponent>();
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
            int numColliders = Physics.OverlapSphereNonAlloc(_playerPosition, 3, _hitColliders, _mask.value | _enemyMask.value);

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
                    else if (_hitColliders[0].TryGetComponent(out Enemy enemy))
                    {
                        //TODO: На будущее
                        // enemy.PackedEntity.Unpack(_world, out int enemyEntity);
                        // OnHitPlayer(enemyEntity,enemy);
                    }

                }
                else
                {
                    aroundComponents = "";
                }

            }
        }

        private void OnHitPlayer(int enemyEntity,Enemy enemy)
        {
            /*if (_damagePool.Has(enemyEntity))
            {
                int damageRequest = _world.NewEntity();
                var damage = _damagePool.Get(enemyEntity).value;
                
                // _world.AddComponentToEntity(damageRequest, new DamageRequestComponent()
                // {
                //     packEntity = enemy.PackedEntity,
                //     damage = damage
                // });
                
                Debug.Log("DamageApproved");
            }*/
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