using Leopotam.EcsLite;
using UnityEngine;

namespace Infrastructure.ECS.Systems
{
    public class FireRateSystem : IEcsInitSystem,IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _firerateFilter;
        private EcsPool<FireRateComponent> _fireratePool;
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _firerateFilter = _world.Filter<FireRateComponent>().End();
            _fireratePool = _world.GetPool<FireRateComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _firerateFilter)
            {
                ref var firetime = ref _fireratePool.Get(entity).firerate;
                firetime += Time.deltaTime;
            }
        }
    }
}