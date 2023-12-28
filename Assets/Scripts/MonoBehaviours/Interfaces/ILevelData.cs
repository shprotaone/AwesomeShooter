using Settings;
using UnityEngine;

namespace MonoBehaviours.Interfaces
{
    public interface ILevelData
    {
        public LevelSettings LevelSettings { get; }
        public Transform SpawnPlayerPoint { get; }
        public WeaponSpawnPoint[] SpawnWeaponPoints { get; }
        public EnemySpawnPoint[] SpawnEnemiesPoints { get; }
    }
}
