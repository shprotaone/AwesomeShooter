using System;
using Cysharp.Threading.Tasks;
using Infrastructure.Factories;
using MonoBehaviours;
using MonoBehaviours.Interfaces;
using Settings;
using UnityEngine;
using Zenject;

namespace Infrastructure.CommonSystems
{
    public class LevelSettingsLoader : ILevelSettingsLoader
    {
        private IGameSceneData _gameSceneData;
        private ICommonSystemsFactory _commonSystemsFactory;
        private IPlayerFactory _playerFactory;

        public IGameSceneData GameSceneData => _gameSceneData;

        public LevelSettingsLoader(ICommonSystemsFactory commonSystemsFactory,IPlayerFactory playerFactory)
        {
            _commonSystemsFactory = commonSystemsFactory;
            _playerFactory = playerFactory;
        }

        public async UniTask<GameObject> LoadLevel()
        {
            return await _playerFactory.LoadLevel();
        }

        public async UniTask<PlayerSettingsSO> GetPlayerSettings()
        {
            return await _playerFactory.GetPlayerSettings();
        }

        public async UniTask<PlayerLevelSettingsSO> GetLevelsStorage()
        {
            return await _playerFactory.GetLevelsStorage();
        }

    }
}