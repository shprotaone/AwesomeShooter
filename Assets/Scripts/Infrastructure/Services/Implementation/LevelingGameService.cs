using System;
using Infrastructure.CommonSystems;
using Settings;
using UnityEngine;

namespace Infrastructure.Services
{
    public class LevelingGameService : ILevelingGameService,IGameService
    {
        public event Action<int> OnUpdateLevel; 
        private LevelSettingsContainer _settingsContainer;

        private PlayerLevelStep _currentLevel;
        private int _currentIndexLevel;

        public LevelingGameService(LevelSettingsContainer settingsContainer,
            IServiceInitializer serviceInitializer)
        {
            _settingsContainer = settingsContainer;
            serviceInitializer.Add(this);
        }

        public void Init()
        {
            _currentIndexLevel = 0;
            _currentLevel = _settingsContainer.PlayerLevelProgress.levels[_currentIndexLevel];
            OnUpdateLevel?.Invoke(_currentLevel.level);
        }

        public void CheckNextLevel(int experienceValue)
        {
            if (experienceValue > _currentLevel.experienceToUp)
            {
                _currentIndexLevel++;
                _currentLevel = _settingsContainer.PlayerLevelProgress.levels[_currentIndexLevel];
                OnUpdateLevel?.Invoke(_currentLevel.level);
            }
        }
    }
}