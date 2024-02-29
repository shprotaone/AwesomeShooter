using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Infrastructure.Services
{
    public class ServiceInitializer : IServiceInitializer
    {
        private List<IGameService> _services;

        public ServiceInitializer()
        {
            _services = new List<IGameService>();
        }

        public void Add(IGameService service)
        {
            _services.Add(service);
        }
        
        public async UniTask Init()
        {
            foreach (IGameService service in _services)
            { 
                service.Init();
            }
        }
    }
}