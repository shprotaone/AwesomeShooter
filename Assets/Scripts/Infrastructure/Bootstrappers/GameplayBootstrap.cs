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

        [Inject]
        public void Construct(SceneStateMachine sceneStateMachine, StatesFactory statesFactory)
        {
            _sceneStateMachine = sceneStateMachine;
            _statesFactory = statesFactory;
        }

        public void Start()
        {
            _sceneStateMachine.RegisterState(_statesFactory.Create<InitGamePlayState>());
            _sceneStateMachine.RegisterState(_statesFactory.Create<PlayGameplayState>());

            _sceneStateMachine.Enter<InitGamePlayState>();
        }
    }
}