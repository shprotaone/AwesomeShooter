using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(menuName = "Settings/LevelSettings")]
    public class LevelSettings : ScriptableObject
    {
        public int enemiesOnLevel;
        public int maxEnemiesOnMap;
        public float minSpawnTimeRate;
        public float maxSpawnTimeRate;
    }
}