using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Infrastructure.CommonSystems;
using Infrastructure.ECS;
using Infrastructure.Factories;
using Infrastructure.GameStates;
using Infrastructure.StateMachines;
using UI;
using Zenject;

namespace Infrastructure.Bootstrappers
{
    public class InitGamePlayState : IState
    {
        private SceneStateMachine _sceneStateMachine;
        private DiContainer _container;
        private ILevelSettingsLoader _levelSettingsLoader;
        private GameplayUIFactory _gameplayUIFactory;
        private EnemyPool _enemyPool;
        private EcsStartup _ecsStartup;

        public InitGamePlayState(SceneStateMachine sceneStateMachine,
            ILevelSettingsLoader levelSettingsLoader,
            EnemyPool enemyPool,
            EcsStartup ecsStartup,
            GameplayUIFactory gameplayUIFactory)
        {
            _sceneStateMachine = sceneStateMachine;
            _levelSettingsLoader = levelSettingsLoader;
            _ecsStartup = ecsStartup;
            _enemyPool = enemyPool;
            _gameplayUIFactory = gameplayUIFactory;
        }
        
        public async UniTask Enter()
        {
            var gameSceneData = await _levelSettingsLoader.LoadGameSceneData();
            await _enemyPool.Init();
            await _ecsStartup.Initialize(gameSceneData);
            _ecsStartup.StartSystems();

           await _gameplayUIFactory.CreateMainHud();
        }

        public async UniTask Exit()
        {
            await _sceneStateMachine.Enter<PlayGameplayState>();
        }
    }
}