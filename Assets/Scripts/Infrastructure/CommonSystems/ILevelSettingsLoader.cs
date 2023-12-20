using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using MonoBehaviours.Interfaces;

namespace Infrastructure.CommonSystems
{
    public interface ILevelSettingsLoader
    {
        IGameSceneData GameSceneData { get; }
        UniTask LoadGameSceneData();
    }
}