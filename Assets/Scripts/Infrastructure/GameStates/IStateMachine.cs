using Cysharp.Threading.Tasks;
using Infrastructure.GameStates;

namespace Scripts.Infrasructure
{
    public interface IStateMachine
    {
        UniTask Enter<TState>() where TState : class, IState;
        UniTask Exit<TState>() where TState : class, IState;
        void RegisterState<TState>(TState state) where TState : IExitState;
    }
}