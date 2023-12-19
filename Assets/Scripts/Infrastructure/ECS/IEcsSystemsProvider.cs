using System.Collections.Generic;
using Leopotam.EcsLite;

namespace Infrastructure.ECS
{
    public interface IEcsSystemsProvider
    {
        public List<IEcsSystem> Systems { get; }
    }
}