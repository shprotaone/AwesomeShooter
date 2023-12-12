using Infrastructure.ECS.Services;
using Infrastructure.ECS.Systems;
using Leopotam.EcsLite;
using UnityEngine;
using Voody.UniLeo.Lite;
using Zenject;

namespace Infrastructure.ECS
{
    sealed class EcsStartup : MonoBehaviour 
    {
        private EcsWorld _world;        
        private EcsSystems _systems;
        private InputService _inputService;

        [Inject]
        private void Construct(InputService inputService)
        {
            _inputService = inputService;
        }
        private void Awake () 
        {
            _world = new EcsWorld();
            _systems = new EcsSystems (_world);
            _systems.ConvertScene();
            AddSystems();
            AddInjection();
            AddOneFrames();
            
            _systems
#if UNITY_EDITOR
                // add debug systems for custom worlds here, for example:
                // .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ("events"))
                .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ());
#endif
            _systems.Init();
        }

        private void AddSystems()
        {
            _systems.Add(new InputSystem(_inputService)).Add(new MovementSystem());
            _systems.Add(new PlayerMouseInputSystem(_inputService));
        }

        private void AddInjection()
        {
            
        }

        private void AddOneFrames()
        {
            
        }

        private void Update () {
            // process systems here.
            _systems?.Run ();
        }

        private void OnDestroy () {
            if (_systems != null) {
                // list of custom worlds will be cleared
                // during IEcsSystems.Destroy(). so, you
                // need to save it here if you need.
                _systems.Destroy ();
                _systems = null;
            }
            
            // cleanup custom worlds here.
            
            // cleanup default world.
            if (_world != null) {
                _world.Destroy ();
                _world = null;
            }
        }
    }
}