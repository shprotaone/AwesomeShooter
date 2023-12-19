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

        public static void ConstructEntity(this int entity,EcsWorld world, List<object> components)
        {
            foreach (var comp in components)
            {
                var component = comp;
                CreateOrAddPool(entity,world,component);
            }
        }

        public static void CreateOrAddPool(int entity,EcsWorld world, object component)
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
    }
}