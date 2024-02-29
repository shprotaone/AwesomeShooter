using LeoEcsPhysics;
using Leopotam.EcsLite;
using UnityEngine;

namespace Infrastructure.ECS.Systems
{
    public class TestCollisionSystem : IEcsInitSystem,IEcsRunSystem
    {

        private EcsWorld _world;
        private EcsFilter _onCollisionEventFilter;
        private EcsPool<OnCollisionEnterEvent> _eventPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _onCollisionEventFilter = _world.Filter<OnCollisionEnterEvent>().End();
            _eventPool = _world.GetPool<OnCollisionEnterEvent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _onCollisionEventFilter)
            {
                ref var eventData = ref _eventPool.Get(entity);

                if (eventData.senderGameObject != null)
                {
                    Debug.Log("Call " + eventData.senderGameObject.name);
                    eventData.collider.gameObject.SetActive(false);
                }

            }
        }
    }
}
