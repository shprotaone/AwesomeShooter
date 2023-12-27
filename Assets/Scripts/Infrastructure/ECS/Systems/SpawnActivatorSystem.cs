using System.Diagnostics;
using System.Linq;
using Infrastructure.ECS.Components;
using Infrastructure.ECS.Components.Tags;
using Leopotam.EcsLite;
using MonoBehaviours;
using TMPro;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Infrastructure.ECS.Systems
{
    public class SpawnActivatorSystem : IEcsInitSystem,IEcsRunSystem
    {
        private EcsWorld _world;

        private EcsFilter _playerFilter;
        private EcsPool<ModelComponent> _playerPool;

        private Transform _playerTransform;
        private Vector3 _extends;
        private float _drawLenght = 60; //TODO:вынести в настройки

        private EnemySpawnPoint[] _selectablePoints;
        private RaycastHit[] _results;

        private Stopwatch sw;

        public void Init(IEcsSystems systems)
        {
            _results = new RaycastHit[10];
            _selectablePoints = new EnemySpawnPoint[10];

            _extends = new Vector3(20, 4, 1);

            _world = systems.GetWorld();
            _playerFilter = _world.Filter<ModelComponent>().Inc<PlayerTag>().End();
            _playerPool = _world.GetPool<ModelComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int player in _playerFilter)
            {
                ref var playerPos = ref _playerPool.Get(player);
                _playerTransform = playerPos.modelTransform;
            }

            CheckNeareastSpawner();
        }

        private void CheckNeareastSpawner()
        {
            sw = new Stopwatch();
            sw.Start();

            if (_playerTransform != null)
            {
                _results = Physics.BoxCastAll(_playerTransform.position, _extends,_playerTransform.forward,
                    Quaternion.identity,_drawLenght,Masks.SpawnPoint);
            }

            if (_results.Length > 0)
            {
                for (int i = 0; i < _results.Length; i++)
                {
                    var point = _results[i].transform.GetComponent<EnemySpawnPoint>();

                    bool contains = CheckSelectable(point);
                    if (!contains) _selectablePoints[i] = point;
                }
            }

            CheckUnselectableV2(_results.Length);
            sw.Stop();
            Debug.Log("Active points " + _results.Length);
        }

        private void CheckUnselectableV2(int index)
    {
        for (int i = index; i < _selectablePoints.Length; i++)
        {
            var point = _selectablePoints[i];

            if (point != null)
            {
                point.Disable();
            }

            _selectablePoints[i] = null;
        }
    }

    private bool CheckSelectable(EnemySpawnPoint spawnPoint)
    {
        if (!_selectablePoints.Contains(spawnPoint))
        {
            spawnPoint.SetWorks();
            return false;
        }
        return true;
    }

    }
}