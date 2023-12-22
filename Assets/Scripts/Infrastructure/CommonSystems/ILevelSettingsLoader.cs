using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using MonoBehaviours.Interfaces;
using Settings;

namespace Infrastructure.CommonSystems
{
    public interface ILevelSettingsLoader
    {
        IGameSceneData GameSceneData { get; }
        UniTask<IGameSceneData> LoadGameSceneData();
        UniTask<PlayerSettingsSO> GetPlayerSettings();
    }
}