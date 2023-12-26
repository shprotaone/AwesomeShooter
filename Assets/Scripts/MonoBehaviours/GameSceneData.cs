using System.Collections;
using System.Collections.Generic;
using MonoBehaviours.Interfaces;
using Settings;
using UnityEngine;

namespace MonoBehaviours
{
    public class GameSceneData : MonoBehaviour, IGameSceneData
    {
        [SerializeField] private Transform _spawnPlayerPoint;
        [SerializeField] private WeaponSpawnPoint[] _spawnWeaponPoints;
        [SerializeField] private EnemySpawnPoint[] _spawnEnemiesPoints;

        public Transform SpawnPlayerPoint => _spawnPlayerPoint;
        public WeaponSpawnPoint[] SpawnWeaponPoints => _spawnWeaponPoints;
        public EnemySpawnPoint[] SpawnEnemiesPoints => _spawnEnemiesPoints;
    }
}