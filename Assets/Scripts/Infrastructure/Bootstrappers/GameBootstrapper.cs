using Infrastructure.Factories;
using Infrastructure.GameStates;
using Infrastructure.StateMachines;
using UnityEngine;
using Zenject;

namespace Infrastructure.Bootstrappers
{
    public class GameBootstrapper : MonoBehaviour
    {
        private GameStateMachine _stateMachine;
        private StatesFactory _statesFactory;

        [Inject]
        private void Construct(GameStateMachine stateMachine,StatesFactory statesFactory)
        {
            _stateMachine = stateMachine;
            _statesFactory = statesFactory;
        }

        private void Start()
        {
            _stateMachine.RegisterState(_statesFactory.Create<GameBootstrapState>());
            _stateMachine.RegisterState(_statesFactory.Create<GameLoadingState>());
            _stateMachine.RegisterState(_statesFactory.Create<GameHubState>());
            _stateMachine.RegisterState(_statesFactory.Create<GameplayState>());
            
            _stateMachine.Enter<GameBootstrapState>();
            DontDestroyOnLoad(this);
        }
    }
}

