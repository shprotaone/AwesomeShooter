using UnityEngine;

namespace MonoBehaviours.Interfaces
{
    public interface IGameSceneData
    {
        public Transform SpawnPlayerPoint { get; }
        public Transform[] SpawnWeaponPoints { get; }
        public Transform[] SpawnEnemiesPoints { get; }
    }
}
