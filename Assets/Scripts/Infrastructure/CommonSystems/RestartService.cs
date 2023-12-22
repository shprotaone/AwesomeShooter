using System;
using System.Collections.Generic;

namespace Infrastructure.CommonSystems
{
    public class RestartService : IDisposable
    {
        private readonly List<IRestartble> restartbles;

        public RestartService(List<IRestartble> restartbles)
        {
            this.restartbles = restartbles;
        }

        public void Dispose()
        {
            foreach (IRestartble restartble in restartbles)
            {
                restartble.Restart();
            }
        }
    }
}