using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Infrastructure.CommonSystems;
using Infrastructure.ECS.Services;
using Leopotam.EcsLite;
using MonoBehaviours.Interfaces;
using Settings;
using UnityEngine;
using Voody.UniLeo.Lite;
using Zenject;

namespace Infrastructure.ECS
{
    public class EcsStartup : IECSRunner,IRestartble
    {
        private EcsWorld _world;
        private EcsSystems _updateSystems;
        private EcsSystems _fixedUpdateSystems;
        private IEcsUpdateSystems _ecsUpdateSystems;
        private IEcsFixedSystems _ecsFixedSystems;
        private IGameSceneData _gameSceneData;
        private DiContainer _container;

        private bool _isInitialized;

        public EcsWorld CurrentWorld => _world;

        private EcsStartup(IEcsUpdateSystems updateSystems, IEcsFixedSystems ecsFixedSystems,DiContainer container)
        {
            _ecsUpdateSystems = updateSystems;
            _ecsFixedSystems = ecsFixedSystems;
            _container = container;
        }

        public async UniTask Initialize(IGameSceneData gameSceneData)
        {
            Debug.Log("StartInit");
            _world = new EcsWorld();
            _container.BindInstance(_world).AsSingle();
            
            _gameSceneData = gameSceneData;
        }

        public void StartSystems()
        {
            AddSystems();
            _updateSystems.ConvertScene();
            _fixedUpdateSystems.ConvertScene();
            _updateSystems.Init();
            _fixedUpdateSystems.Init();
            _isInitialized = true;
        }

        private void AddSystems()
        {
            _updateSystems = GetSystems(_ecsUpdateSystems.Systems);
            _updateSystems
#if UNITY_EDITOR
                .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ());
#endif
            _fixedUpdateSystems = GetSystems(_ecsFixedSystems.Systems);
        }

        public void Tick()
        {
            if(_isInitialized)
                _updateSystems?.Run ();
        }

        public void FixedTick()
        {
            if(_isInitialized)
                _fixedUpdateSystems?.Run();
        }

        public void Dispose()
        {
            if (_updateSystems != null)
            {
                _updateSystems.Destroy();
                _updateSystems = null;
            }

            if (_fixedUpdateSystems != null)
            {
                _fixedUpdateSystems.Destroy();
                _fixedUpdateSystems = null;
            }

            if (_world != null)
            {
                _world.Destroy();
                _world = null;
            }
        }

        private EcsSystems GetSystems(List<IEcsSystem> bindingSystems)
        {
            EcsSystems systems = new EcsSystems(_world,_gameSceneData);
            foreach (var s in bindingSystems)
            {
                systems.Add(s);
            }

            return systems;
        }

        public void Restart()
        {
            Dispose();
        }
    }
}