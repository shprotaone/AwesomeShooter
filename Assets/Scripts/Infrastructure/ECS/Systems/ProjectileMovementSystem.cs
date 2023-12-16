using System;
using Infrastructure.ECS.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Infrastructure.ECS.Systems
{
    public class ProjectileMovementSystem : IEcsInitSystem,IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _projectileFilter;
        private EcsPool<ProjectileComponent> _projectilePool;
        private EcsPool<LifeTimeComponent> _lifetimePool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _projectileFilter = _world.Filter<ProjectileComponent>().Inc<LifeTimeComponent>().End();

            _projectilePool = _world.GetPool<ProjectileComponent>();
            _lifetimePool = _world.GetPool<LifeTimeComponent>();

            Debug.Log("Entity In World" + _world.GetEntitiesCount());
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _projectileFilter)
            {
                ref var projectileComponent = ref _projectilePool.Get(entity);
                ref var bullet = ref _projectilePool.Get(entity).projectile;

                if (projectileComponent.projectile.IsActive)
                {
                    projectileComponent.position +=
                        projectileComponent.speed * Time.deltaTime * bullet.transform.forward;

                    bullet.SetPosition(projectileComponent.position);

                    ref var lifetime = ref _lifetimePool.Get(entity).lifeTime;

                    if (lifetime <= 0)
                    {
                        bullet.DisableBullet();
                        _world.DelEntity(entity);
                    }
                }
            }
        }
    }
}