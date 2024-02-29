using System;
using Cysharp.Threading.Tasks;
using Infrastructure.CommonSystems;
using Infrastructure.ECS;
using Infrastructure.Factories;
using Infrastructure.GameStates;
using Infrastructure.Services;
using Infrastructure.StateMachines;
using MonoBehaviours.Interfaces;
using UI;
using UnityEngine;

namespace Infrastructure.Bootstrappers
{
    public class InitGamePlayState : IState
    {
        private SceneStateMachine _sceneStateMachine;
        private ILevelSettingsLoader _levelSettingsLoader;
        private IServiceInitializer _serviceInitializer;
        private LevelSettingsContainer _levelSettings;
        private GameplayUIFactory _gameplayUIFactory;
        private UIService _uiService;
        private EnemyPool _enemyPool;
        private EcsStartup _ecsStartup;

        private GameObject _level;

        public InitGamePlayState(SceneStateMachine sceneStateMachine,
            ILevelSettingsLoader levelSettingsLoader,
            LevelSettingsContainer levelSettingsContainer,
            EnemyPool enemyPool,
            EcsStartup ecsStartup,
            GameplayUIFactory gameplayUIFactory,
            IServiceInitializer serviceInitializer)
        {
            _sceneStateMachine = sceneStateMachine;
            _levelSettingsLoader = levelSettingsLoader;
            _ecsStartup = ecsStartup;
            _enemyPool = enemyPool;
            _gameplayUIFactory = gameplayUIFactory;
            _levelSettings = levelSettingsContainer;
            _serviceInitializer = serviceInitializer;
        }

        public async UniTask Enter()
        {
            var gameSceneData = await SetUpLevelSettings();
            await _enemyPool.Init();
            await _ecsStartup.Initialize(gameSceneData);
            await _ecsStartup.StartSystems();

            await _gameplayUIFactory.CreateMainHud();
            await _serviceInitializer.Init();
            await _sceneStateMachine.Enter<PlayGameplayState>();
        }

        public async UniTask Exit()
        {


        }

        private async UniTask<ILevelData> SetUpLevelSettings()
        {
            var gameSceneData = await _levelSettingsLoader.LoadLevel();
            var playerSettings = await _levelSettingsLoader.GetPlayerSettings();
            var playerLevels = await _levelSettingsLoader.GetLevelsStorage();

            _levelSettings.SetPlayerSettings(playerSettings);
            _levelSettings.SetPlayerLevelSettings(playerLevels);
            return gameSceneData;
        }
    }
}