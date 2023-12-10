using Cysharp.Threading.Tasks;

namespace Infrastructure.GameStates
{
    public interface ISceneLoader
    {
        UniTask Load(string nextScene);
    }
}