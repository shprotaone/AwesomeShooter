using Cysharp.Threading.Tasks;
using Infrastructure.CommonSystems;
using Infrastructure.ECS;
using Infrastructure.Factories;
using Infrastructure.GameStates;
using Infrastructure.StateMachines;
using MonoBehaviours.Interfaces;
using UI;

namespace Infrastructure.Bootstrappers
{
    public class InitGamePlayState : IState
    {
        private SceneStateMachine _sceneStateMachine;
        private ILevelSettingsLoader _levelSettingsLoader;
        private LevelSettingsContainer _levelSettings;
        private GameplayUIFactory _gameplayUIFactory;
        private EnemyPool _enemyPool;
        private EcsStartup _ecsStartup;

        public InitGamePlayState(SceneStateMachine sceneStateMachine,
            ILevelSettingsLoader levelSettingsLoader,
            LevelSettingsContainer levelSettingsContainer,
            EnemyPool enemyPool,
            EcsStartup ecsStartup,
            GameplayUIFactory gameplayUIFactory)
        {
            _sceneStateMachine = sceneStateMachine;
            _levelSettingsLoader = levelSettingsLoader;
            _ecsStartup = ecsStartup;
            _enemyPool = enemyPool;
            _gameplayUIFactory = gameplayUIFactory;
            _levelSettings = levelSettingsContainer;
        }

        public async UniTask Enter()
        {
            var gameSceneData = await SetUpLevelSettings();
            await _enemyPool.Init();
            await _ecsStartup.Initialize(gameSceneData);
            _ecsStartup.StartSystems();

            await _gameplayUIFactory.CreateMainHud();
            await _sceneStateMachine.Enter<PlayGameplayState>();

        }

        public async UniTask Exit()
        {


        }

        private async UniTask<IGameSceneData> SetUpLevelSettings()
        {
            var gameSceneData = await _levelSettingsLoader.LoadGameSceneData();
            var playerSettings = await _levelSettingsLoader.GetPlayerSettings();
            var playerLevels = await _levelSettingsLoader.GetLevelsStorage();

            _levelSettings.SetPlayerSettings(playerSettings);
            _levelSettings.SetPlayerLevelSettings(playerLevels);
            return gameSceneData;
        }
    }
}