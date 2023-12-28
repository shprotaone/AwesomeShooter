using System.Collections.Generic;

namespace Infrastructure.Services
{
    public interface IServiceInitializer
    {
        List<IService> Services { get; }
        void Init();
    }
}