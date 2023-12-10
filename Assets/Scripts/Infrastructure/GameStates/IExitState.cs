using Cysharp.Threading.Tasks;

namespace Infrastructure.GameStates
{
    public interface IExitState
    {
        UniTask Exit();
    }
}