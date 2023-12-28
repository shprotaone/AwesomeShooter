using System;

namespace Infrastructure.Services
{
    public class LevelProgressService : ILevelProgressService,IService
    {
        public event Action<int> OnLeftEnemy;
        private int _leftEnemy;
        
        public int LeftEnemy => _leftEnemy;

        public LevelProgressService(IServiceInitializer serviceInitializer)
        {
            serviceInitializer.Services.Add(this);
        }

        public void Init()
        {
            OnLeftEnemy?.Invoke(_leftEnemy);
        }

        public void SetEnemiesInLevel(int levelSettingsEnemiesOnLevel)
        {
            _leftEnemy = levelSettingsEnemiesOnLevel;
            
        }

        public void DecreaseLeftEnemy()
        {
            if (_leftEnemy > 0)
            {
                _leftEnemy--;    
            }
            OnLeftEnemy?.Invoke(_leftEnemy);
        }
    }
}