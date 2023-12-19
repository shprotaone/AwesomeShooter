using System.Collections.Generic;
using System.Linq;
using Extention;
using Infrastructure.ECS.Components.Tags;
using Leopotam.EcsLite;
using TMPro;
using UnityEngine;

namespace Infrastructure.ECS.Components
{
    public class PickUpSystem : IEcsInitSystem,IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _pickUpFilter;
        private EcsFilter _playerFilter;
        private EcsPool<PickUpComponent> _pickUpPool;
        private EcsPool<ModelComponent> _playerPool;

        private Vector3 _playerPosition;
        private LayerMask _mask;

        public void Init(IEcsSystems systems)
        {
            _playerPosition = new Vector3();
            _mask = Masks.Pickable;
            _world = systems.GetWorld();
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
            Collider[] hitColliders = new Collider[3];
            int numColliders = Physics.OverlapSphereNonAlloc(_playerPosition, 1, hitColliders, _mask.value);
            Debug.Log(numColliders);

            if (numColliders > 0)
            {
                foreach (int entity in _pickUpFilter)
                {
                    ref var isPicked = ref _pickUpPool.Get(entity).isPicked;

                    if (!isPicked)
                    {
                        isPicked = true;
                        OnTriggerEnter(hitColliders.First(), entity);
                    }

                }
            }
        }

        private void OnTriggerEnter(Collider other,int entity)
        {
            int itemEntity = entity;
            ItemType type = itemEntity.GetECSComponent<ItemTypeComponent>(_world).type;
            EcsPackedEntity itemPackEntity = _world.PackEntity(itemEntity);
            int requestEntity = _world.NewEntity();
            requestEntity.ConstructEntity(_world,new List<object>
            {
                new PickUpRequest()
                {
                    entity = itemPackEntity,
                    itemType = type
                }
            });

            Debug.Log("Enter " + other.gameObject);
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