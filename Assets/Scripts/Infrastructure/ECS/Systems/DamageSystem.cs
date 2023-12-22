using Extention;
using Infrastructure.ECS.Components;
using Leopotam.EcsLite;

namespace Infrastructure.ECS.Systems
{
    public class DamageSystem : IEcsInitSystem,IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private EcsPool<HealthComponent> _healthPool;
        private EcsPool<DamageRequestComponent> _damageRequestPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _filter = _world.Filter<DamageRequestComponent>()
                .End();

            _healthPool = _world.GetPool<HealthComponent>();
            _damageRequestPool = _world.GetPool<DamageRequestComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var requestEntity in _filter)
            {
                ref var damageRequest = ref _damageRequestPool.Get(requestEntity);
                ref var damage = ref damageRequest.damage;
                damageRequest.packEntity.Unpack(_world,out int health);

                ref var damagedHealth = ref _healthPool.Get(health).health;
                damagedHealth -= damage;

                if (damagedHealth <= 0)
                {
                    int deathRequest = _world.NewEntity();
                    _world.AddComponentToEntity(deathRequest,new DeathEventComponent
                    {
                        packedEntity = _world.PackEntity(requestEntity)
                    });
                }

                _damageRequestPool.Del(requestEntity);
            }
        }
    }
}