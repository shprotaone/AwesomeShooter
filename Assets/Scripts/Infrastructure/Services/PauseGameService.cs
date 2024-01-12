using System.Collections.Generic;

namespace Infrastructure.Services
{
    public class PauseGameService : IGameService
    {
        private List<IPaused> _pausedSystems;

        public PauseGameService(IServiceInitializer serviceInitializer)
        {
            _pausedSystems = new List<IPaused>();
            serviceInitializer.Add(this);
        }

        public void Init()
        {
            SetPaused(false);
        }

        public void SetPaused(bool flag)
        {
            foreach (IPaused system in _pausedSystems)
            {
                system.IsPaused = flag;
            }
        }

        public void Add(IPaused paused)
        {
            _pausedSystems.Add(paused);
        }
    }
}