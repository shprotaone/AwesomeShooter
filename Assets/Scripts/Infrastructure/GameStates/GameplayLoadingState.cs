using Cysharp.Threading.Tasks;
using Infrastructure.GameStates;
using Infrastructure.StateMachines;

namespace Infrastructure.Factories
{
    public class GameplayLoadingState : IState
    {
        private GameStateMachine _gameStateMachine;
        public UniTask Enter()
        {
            throw new System.NotImplementedException();
        }

        public UniTask Exit()
        {
            throw new System.NotImplementedException();
        }
    }
}