using Settings;
using UnityEngine;

namespace MonoBehaviours.Interfaces
{
    public interface IGameSceneData
    {
        public Transform SpawnPlayerPoint { get; }
        public WeaponSpawnPoint[] SpawnWeaponPoints { get; }
        public EnemySpawnPoint[] SpawnEnemiesPoints { get; }
    }
}
