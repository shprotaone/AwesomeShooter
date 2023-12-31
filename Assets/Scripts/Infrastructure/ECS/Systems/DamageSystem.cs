﻿using Extention;
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
                damageRequest.packEntity.Unpack(_world,out int entityWithHealth);

                ref var healthComponent = ref _healthPool.Get(entityWithHealth);
                ref var health = ref healthComponent.health;
                
                health -= damage;
                healthComponent.OnHealthChanged?.Invoke(health);

                _world.DelEntity(requestEntity);
                if (health <= 0)
                {
                    int deathRequest = _world.NewEntity();
                    _world.AddComponentToEntity(deathRequest,new DeathRequestComponent
                    {
                        packedEntity = _world.PackEntity(entityWithHealth)
                    });
                }
            }
        }
    }
}