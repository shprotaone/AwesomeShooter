using UnityEngine;

namespace Infrastructure.Services
{
    public class TimeService
    {
        private float timeScale;

        public float CurrentTime => Time.time;
        public float DeltaTime => Time.deltaTime;
        public bool IsPaused => Time.timeScale == 0;

        public TimeService()
        {
            timeScale = Time.timeScale;
        }

    }
}