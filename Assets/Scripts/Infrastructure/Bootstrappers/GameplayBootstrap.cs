using Infrastructure.CommonSystems;
using Infrastructure.Factories;
using Infrastructure.StateMachines;
using UnityEngine;
using Zenject;

namespace Infrastructure.Bootstrappers
{
    public class GameplayBootstrap : MonoBehaviour
    {
        private SceneStateMachine _sceneStateMachine;
        private StatesFactory _statesFactory;
        private IInstantiator _instantiator;

        [Inject]
        public void Construct(SceneStateMachine sceneStateMachine, StatesFactory statesFactory,IInstantiator instantiator)
        {
            _sceneStateMachine = sceneStateMachine;
            _statesFactory = statesFactory;
        }

        public async void Start()
        {
            _sceneStateMachine.RegisterState(_statesFactory.Create<InitGamePlayState>());
            _sceneStateMachine.RegisterState(_statesFactory.Create<PlayGameplayState>());

            await _sceneStateMachine.Enter<InitGamePlayState>();
        }
    }
}