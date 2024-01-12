using Extention;
using Infrastructure.ECS.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Infrastructure.ECS.Systems
{
    public class PeriodicDamageSystem : IEcsInitSystem,IEcsRunSystem
    {
        private EcsWorld _ecsWorld;
        private EcsFilter _filter;
        
        private EcsPool<HealthComponent> _healthPool;
        private EcsPool<FireRateComponent> _fireRatePool;
        private EcsPool<PeriodicDamageRequestComponent> _damageRequestPool;
        
        public void Init(IEcsSystems systems)
        {
            _ecsWorld = systems.GetWorld();
            _filter = _ecsWorld.Filter<PeriodicDamageRequestComponent>().Inc<FireRateComponent>().End();
            _fireRatePool = _ecsWorld.GetPool<FireRateComponent>();
            _healthPool = _ecsWorld.GetPool<HealthComponent>();
            _damageRequestPool = _ecsWorld.GetPool<PeriodicDamageRequestComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int damageRequest in _filter)
            {
                ref var perDamage = ref _damageRequestPool.Get(damageRequest);
                perDamage.PackedEntity.Unpack(_ecsWorld, out int entityWithHealth);
                ref var firerate = ref _fireRatePool.Get(damageRequest).firerate;

                ref var healthComponent = ref _healthPool.Get(entityWithHealth);
                ref var health = ref healthComponent.health;
                firerate -= Time.deltaTime;
                
                if (firerate <= 0)
                {
                    health -= perDamage.Damage;
                    firerate = perDamage.TimeOfDamage;
                }
                
                if (health <= 0)
                {
                    int deathRequest = _ecsWorld.NewEntity();
                    _ecsWorld.AddComponentToEntity(deathRequest,new DeathRequestComponent
                    {
                        packedEntity = _ecsWorld.PackEntity(entityWithHealth)
                    });
                }

            }
        }
    }
}