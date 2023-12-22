using System;
using Zenject;

namespace Infrastructure.ECS
{
    public interface IECSRunner :ITickable, IFixedTickable, IDisposable
    {

    }
}