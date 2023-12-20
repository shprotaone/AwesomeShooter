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
        private IInstantiator _instantiator;

        public InitGamePlayState(SceneStateMachine sceneStateMachine,
            DiContainer container,
            IInstantiator instantiator,
            ILevelSettingsLoader levelSettingsLoader)
        {
            _sceneStateMachine = sceneStateMachine;
            _container = container;
            _levelSettingsLoader = levelSettingsLoader;
            _instantiator = instantiator;
        }
        
        public async UniTask Enter()
        {
            await _levelSettingsLoader.LoadGameSceneData();
            ECSUpdateSystemsInstaller.Install(_container);
            ECSFixedUpdateSystemsInstaller.Install(_container);
            var ecs = _instantiator.Instantiate<EcsStartup>();
            ecs.Initialize();
            ecs.StartSystems();
        }

        public UniTask Exit()
        {
            throw new NotImplementedException();
        }
    }
}