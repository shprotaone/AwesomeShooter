using System;
using Cysharp.Threading.Tasks;
using Infrastructure.CommonSystems;
using Infrastructure.ECS;
using Infrastructure.GameStates;
using Infrastructure.StateMachines;
using Zenject;

namespace Infrastructure.Bootstrappers
{
    public class InitGamePlayState : IState
    {
        private SceneStateMachine _sceneStateMachine;
        private DiContainer _container;
        private ILevelSettingsLoader _levelSettingsLoader;
        private EnemyPool _enemyPool;
        private EcsStartup _ecsStartup;
        public InitGamePlayState(SceneStateMachine sceneStateMachine,
            ILevelSettingsLoader levelSettingsLoader,
            EnemyPool enemyPool,
        EcsStartup ecsStartup)
        {
            _sceneStateMachine = sceneStateMachine;
            _levelSettingsLoader = levelSettingsLoader;
            _ecsStartup = ecsStartup;
            _enemyPool = enemyPool;
        }
        
        public async UniTask Enter()
        {
            var gameSceneData = await _levelSettingsLoader.LoadGameSceneData();
            await _enemyPool.Init();
            await _ecsStartup.Initialize(gameSceneData);
            _ecsStartup.StartSystems();
        }

        public UniTask Exit()
        {
            throw new NotImplementedException();
        }
    }
}