using System.Collections;
using System.Collections.Generic;
using MonoBehaviours.Interfaces;
using UnityEngine;

namespace MonoBehaviours
{
    public class GameSceneData : MonoBehaviour, IGameSceneData
    {
        [SerializeField] private Transform _spawnPlayerPoint;
        [SerializeField] private Transform[] _spawnWeaponPoints;
        [SerializeField] private Transform[] _spawnEnemiesPoints;

        public Transform SpawnPlayerPoint => _spawnPlayerPoint;
        public Transform[] SpawnWeaponPoints => _spawnWeaponPoints;
        public Transform[] SpawnEnemiesPoints => _spawnEnemiesPoints;
    }
}