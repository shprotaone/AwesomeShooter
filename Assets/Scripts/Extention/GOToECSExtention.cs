using System;
using System.Collections.Generic;
using Leopotam.EcsLite;

namespace Extention
{
    public static class GOToECSExtention
    {
        public static T GetECSComponent<T>(this int entity, EcsWorld world) where T : struct
        {
            return world.GetPool<T>().Get(entity);
        }

        public static void ConstructEntity(int entity, EcsWorld world, List<object> components)
        {
            foreach (var comp in components)
            {
                var component = comp;
                AddComponentToEntity(entity,world,component);
            }
        }

        public static void AddComponentToEntity(int entity,EcsWorld world, object component)
        {
            var componentType = component.GetType();

            var pool = world.GetPoolByType(componentType);

            if (pool == null)
            {
                var meth = world.GetType().GetMethod(nameof(EcsWorld.GetPool));
                var generic = meth.MakeGenericMethod(componentType);
                pool = (IEcsPool)generic.Invoke(world, null);
            }

            if (!pool.Has(entity))
            {
                pool.AddRaw(entity,component);
            }
        }

        public static int NewEntityWithComponents(this EcsWorld world, List<object> components)
        {
            var entity = world.NewEntity();
            ConstructEntity(entity,world,components);
            return entity;
        }

        public static void AddComponentToEntity<TComponent>(this EcsWorld world, int entity, TComponent component)
            where TComponent : struct
        {
            var pool = world.GetPool<TComponent>();
            pool.Add(entity);
            ref var reference = ref pool.Get(entity);
            reference = component;
        }
    }
}