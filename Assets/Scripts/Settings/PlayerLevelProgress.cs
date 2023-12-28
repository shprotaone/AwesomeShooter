using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Settings
{
    [CreateAssetMenu(menuName = "Settings/LevelSettings")]
    public class PlayerLevelProgress : ScriptableObject
    {
        public List<PlayerLevelStep> levels;
    }
}