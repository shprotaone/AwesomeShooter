using System;

namespace Infrastructure.Services
{
    public interface ILevelProgressService
    {
        public event Action<int> OnLeftEnemy;
        int LeftEnemy { get; }
        void DecreaseLeftEnemy();
        void SetEnemiesInLevel(int levelSettingsEnemiesOnLevel);
    }
}