using Cysharp.Threading.Tasks;
using Infrastructure.CommonSystems;
using MonoBehaviours;
using MonoBehaviours.Interfaces;

namespace Infrastructure.Factories
{
    public interface ICommonSystemsFactory
    {
        UniTask InitializeCurtainLoadingAsync();
        UniTask<IGameSceneData> GetGameSceneData();
    }
}