using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Infrastructure.CommonSystems;
using Infrastructure.ECS.Services;
using Infrastructure.ECS.Systems;
using Infrastructure.Services;
using LeoEcsPhysics;
using Leopotam.EcsLite;
using MonoBehaviours.Interfaces;
using Settings;
using UnityEngine;
using Voody.UniLeo.Lite;
using Zenject;

namespace Infrastructure.ECS
{
    public class EcsStartup : IECSRunner,IPaused
    {
        private EcsWorld _world;
        private EcsSystems _updateSystems;
        private EcsSystems _fixedUpdateSystems;
        private IEcsUpdateSystems _ecsUpdateSystems;
        private IEcsFixedSystems _ecsFixedSystems;
        private ILevelData _levelData;
        private DiContainer _container;

        private bool _isInitialized;

        public EcsWorld CurrentWorld => _world;
        public bool IsPaused { get; set; }


        private EcsStartup(IEcsUpdateSystems updateSystems, 
            IEcsFixedSystems ecsFixedSystems,
            DiContainer container,
            PauseGameService pauseGameService)
        {
            _ecsUpdateSystems = updateSystems;
            _ecsFixedSystems = ecsFixedSystems;
            _container = container;
            pauseGameService.Add(this);
        }

        public async UniTask Initialize(ILevelData levelData)
        {

            _world = new EcsWorld();
            _container.BindInstance(_world).AsSingle();
            
            _levelData = levelData;
        }

        public async UniTask StartSystems()
        {
            AddSystems();
            _updateSystems.ConvertScene();
            _fixedUpdateSystems.ConvertScene();

            EcsPhysicsEvents.ecsWorld = _world;

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
            if(_isInitialized && !IsPaused)
                _updateSystems?.Run ();
        }

        public void FixedTick()
        {
            if(_isInitialized && !IsPaused)
                _fixedUpdateSystems?.Run();
        }

        public void Dispose()
        {
            EcsPhysicsEvents.ecsWorld = null;

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
            EcsSystems systems = new EcsSystems(_world,_levelData);
            foreach (var s in bindingSystems)
            {
                systems.Add(s);
            }

            return systems;
        }
    }
}