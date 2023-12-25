using System.Collections.Generic;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(menuName = "Settings/LevelSettings")]
    public class PlayerLevelSettingsSO :ScriptableObject
    {
        public List<PlayerLevelStep> _levels;
    }
}