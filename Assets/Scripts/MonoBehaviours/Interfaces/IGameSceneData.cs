using Settings;
using UnityEngine;

namespace MonoBehaviours.Interfaces
{
    public interface IGameSceneData
    {
        public PlayerSettingsSO PlayerSettingsSo { get; }
        public Transform SpawnPlayerPoint { get; }
        public WeaponSpawnPoint[] SpawnWeaponPoints { get; }
        public EnemySpawnPoint[] SpawnEnemiesPoints { get; }
    }
}
