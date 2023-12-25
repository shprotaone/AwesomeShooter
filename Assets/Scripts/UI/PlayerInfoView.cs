using System;
using Settings;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class PlayerInfoView : MonoBehaviour
    {
        [SerializeField] private Slider _hpSlider;
        [SerializeField] private Slider _expSLider;

        private PlayerLevelSettingsSO _levelSettingsSo;
        [Inject]
        private void Construct(PlayerLevelSettingsSO levelSettingsSo)
        {
            _levelSettingsSo = levelSettingsSo;
        }
        
        public void SetUpHP(float hp)
        {
            _hpSlider.maxValue = hp;
            _hpSlider.value = hp;
        }

        public void SetUpExp(int currentLevel)
        {
            _expSLider.maxValue = _levelSettingsSo._levels.
                Find(x => x.level == currentLevel).experienceToUp;
        }

        public void ChangeHealth(float obj)
        {
            _hpSlider.value = obj;
            Debug.Log("HealthChanged");
        }

        public void ChangeExp(float obj)
        {
            _expSLider.value = obj;
        }
    }
}