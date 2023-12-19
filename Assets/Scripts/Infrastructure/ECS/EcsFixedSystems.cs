using System;
using System.Collections.Generic;
using System.Linq;
using Leopotam.EcsLite;

namespace Infrastructure.ECS
{
    public class EcsFixedSystems : IEcsFixedSystems, IDisposable
    {
        private List<IEcsSystem> _systems;
        public List<IEcsSystem> Systems => _systems;

        public EcsFixedSystems(IEnumerable<IEcsSystem> systems)
        {
            _systems = systems.ToList();
        }
        public void Dispose()
        {
            _systems.Clear();
        }
    }
}