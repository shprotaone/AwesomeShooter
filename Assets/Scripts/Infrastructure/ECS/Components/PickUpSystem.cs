using System;
using System.Collections.Generic;
using System.Linq;
using Extention;
using Infrastructure.ECS.Components;
using Leopotam.EcsLite;
using UnityEngine;
using Voody.UniLeo.Lite;
using Zenject;

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

        _playerFilter = _world.Filter<ModelComponent>().End();
        _playerPool = _world.GetPool<ModelComponent>();
    }

    public void Run(IEcsSystems systems)
    {
        SetOverlapPosition();
    }

    private void SetOverlapPosition()
    {
        foreach (int player in _playerFilter)
        {
            ref var playerPos = ref _playerPool.Get(player).modelTransform;
            _playerPosition = playerPos.position;
        }
        Collider[] hitColliders = new Collider[3];
        int numColliders = Physics.OverlapSphereNonAlloc(_playerPosition, 1, hitColliders, _mask.value);
        Debug.Log(numColliders);
        foreach (int entity in _pickUpFilter)
        {
            OnTriggerEnter(hitColliders.First(),entity);
        }
    }

    private void OnTriggerEnter(Collider other,int entity)
    {
        int itemEntity = entity;
        ItemType type = itemEntity.GetComponents<ItemTypeComponent>(_world).type;
        EcsPackedEntity itemPackEntity = _world.PackEntity(itemEntity);
        int requestEntity = GOToECSExtention.CreateEntity(_world);
        requestEntity.ConstructEntity(_world,new List<object>
        {
            new PickUpRequest()
            {
                entity = itemPackEntity,
                itemType = type
            }
        });

        _world.GetPool<PickUpRequest>().Add(requestEntity);

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