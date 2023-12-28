using System.Collections;
using System.Collections.Generic;
using MonoBehaviours.Interfaces;
using Settings;
using UnityEngine;

namespace MonoBehaviours
{
    public class LevelData : MonoBehaviour, ILevelData
    {
        [SerializeField] private LevelSettings _levelSettings;
        [SerializeField] private Transform _spawnPlayerPoint;
        [SerializeField] private WeaponSpawnPoint[] _spawnWeaponPoints;
        [SerializeField] private EnemySpawnPoint[] _spawnEnemiesPoints;

        public LevelSettings LevelSettings => _levelSettings;
        public Transform SpawnPlayerPoint => _spawnPlayerPoint;
        public WeaponSpawnPoint[] SpawnWeaponPoints => _spawnWeaponPoints;
        public EnemySpawnPoint[] SpawnEnemiesPoints => _spawnEnemiesPoints;
    }
}