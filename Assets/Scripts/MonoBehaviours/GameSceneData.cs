using System.Collections;
using System.Collections.Generic;
using MonoBehaviours.Interfaces;
using Settings;
using UnityEngine;

namespace MonoBehaviours
{
    public class GameSceneData : MonoBehaviour, IGameSceneData
    {
        [SerializeField] private PlayerSettingsSO _playerSettings;
        [SerializeField] private Transform _spawnPlayerPoint;
        [SerializeField] private WeaponSpawnPoint[] _spawnWeaponPoints;
        [SerializeField] private EnemySpawnPoint[] _spawnEnemiesPoints;

        public PlayerSettingsSO PlayerSettingsSo => _playerSettings;
        public Transform SpawnPlayerPoint => _spawnPlayerPoint;
        public WeaponSpawnPoint[] SpawnWeaponPoints => _spawnWeaponPoints;
        public EnemySpawnPoint[] SpawnEnemiesPoints => _spawnEnemiesPoints;
    }
}