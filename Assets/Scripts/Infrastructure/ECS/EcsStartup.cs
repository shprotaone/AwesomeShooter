using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Infrastructure.CommonSystems;
using Infrastructure.ECS.Services;
using Leopotam.EcsLite;
using UnityEngine;
using Voody.UniLeo.Lite;
using Zenject;

namespace Infrastructure.ECS
{
    public class EcsStartup : IInitializable,ITickable,IFixedTickable,IDisposable,IEcsStartupWorld
    {
        private EcsWorld _world;
        private EcsSystems _updateSystems;
        private EcsSystems _fixedUpdateSystems;
        private IEcsUpdateSystems _ecsUpdateSystems;
        private IEcsFixedSystems _ecsFixedSystems;

        private bool _isInitialized;
        
        [Inject]
        private void Construct(InputService inputService,
            BulletPool bulletPool,
            IEcsUpdateSystems updateSystems,
            IEcsFixedSystems ecsFixedSystems)
        {
            _ecsUpdateSystems = updateSystems;
            _ecsFixedSystems = ecsFixedSystems;
        }

        public void Initialize()
        {
            Debug.Log("StartInit");
            _world = new EcsWorld();

            _updateSystems = new EcsSystems(_world);
            _fixedUpdateSystems = new EcsSystems(_world);
            _updateSystems.ConvertScene();
        }

        public void StartSystems()
        {
            AddSystems();
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
            Debug.Log("Dispose");
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
            EcsSystems systems = new EcsSystems(_world);
            foreach (var s in bindingSystems)
            {
                systems.Add(s);
            }

            return systems;
        }
    }
}