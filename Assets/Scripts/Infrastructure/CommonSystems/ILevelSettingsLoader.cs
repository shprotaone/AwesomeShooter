using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using MonoBehaviours.Interfaces;
using Settings;
using UnityEngine;

namespace Infrastructure.CommonSystems
{
    public interface ILevelSettingsLoader
    {
        IGameSceneData GameSceneData { get; }
        UniTask<IGameSceneData> LoadLevel();
        UniTask<PlayerSettingsSO> GetPlayerSettings();
        UniTask<PlayerLevelSettingsSO> GetLevelsStorage();
    }
}