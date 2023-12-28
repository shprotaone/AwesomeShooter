using System;
using System.Collections.Generic;

namespace Infrastructure.Services
{
    public class ServiceInitializer : IServiceInitializer,IDisposable
    {
        public List<IService> Services { get; }

        public ServiceInitializer()
        {
            Services = new List<IService>();
        }
        public void Init()
        {
            foreach (IService service in Services)
            {
                service.Init();
            }
        }

        public void Dispose()
        {
            Services.Clear();
        }
    }
}