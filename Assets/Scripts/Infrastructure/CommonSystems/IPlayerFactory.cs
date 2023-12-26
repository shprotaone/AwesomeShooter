using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Settings;
using UnityEngine;

namespace Infrastructure.CommonSystems
{
    public interface IPlayerFactory
    {
        UniTask<GameObject> GetPlayer();
        UniTask<PlayerSettingsSO> GetPlayerSettings();
        UniTask<PlayerLevelSettingsSO> GetLevelsStorage();
    }
}