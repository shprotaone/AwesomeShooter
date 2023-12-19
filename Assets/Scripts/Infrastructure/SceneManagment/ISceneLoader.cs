using Cysharp.Threading.Tasks;

namespace Infrastructure.SceneManagment
{
    public interface ISceneLoader
    {
        UniTask Load(string nextScene);
    }
}