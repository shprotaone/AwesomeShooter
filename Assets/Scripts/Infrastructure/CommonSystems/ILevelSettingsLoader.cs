using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using MonoBehaviours.Interfaces;
using Settings;
using UnityEngine;

namespace Infrastructure.CommonSystems
{
    public interface ILevelSettingsLoader
    {
        ILevelData LevelData { get; }
        UniTask<ILevelData> LoadLevel();
        UniTask<PlayerSettingsSO> GetPlayerSettings();
        UniTask<PlayerLevelProgress> GetLevelsStorage();
    }
}