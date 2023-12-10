using Cysharp.Threading.Tasks;

namespace Infrastructure.GameStates
{
    public interface IState : IExitState
    {
        UniTask Enter();
    }
}