using System;
using Infrastructure.ECS.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Infrastructure.ECS.Systems
{
    public class ProjectileMovement : IEcsInitSystem,IEcsRunSystem
    {
        private EcsFilter _projectileFilter;
        private EcsPool<ProjectileComponent> _projectilePool;

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            _projectileFilter = world.Filter<ProjectileComponent>().End();
            _projectilePool = world.GetPool<ProjectileComponent>();

            Debug.Log("Entity In World" + world.GetEntitiesCount());
        }

        public void Run(IEcsSystems systems)
        {
            //TODO Обновлять/добавлять количество Entity когда они спавнятся
            foreach (var entity in _projectileFilter)
            {
                ref var projectileComponent = ref _projectilePool.Get(entity);

                if (projectileComponent.transform.gameObject.activeInHierarchy)
                {
                    projectileComponent.transform.position +=
                        projectileComponent.speed * Time.deltaTime * projectileComponent.transform.forward;
                }
            }
        }
    }
}