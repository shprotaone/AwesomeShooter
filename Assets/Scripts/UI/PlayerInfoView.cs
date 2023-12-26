using System;
using Infrastructure.CommonSystems;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class PlayerInfoView : MonoBehaviour,IView
    {
        [SerializeField] private Slider _hpSlider;
        [SerializeField] private Slider _expSLider;

        private LevelSettingsContainer _levelSettingsSo;
        [Inject]
        private void Construct(LevelSettingsContainer levelSettingsSo)
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
            _expSLider.maxValue = _levelSettingsSo.PlayerLevelSettingsSo.levels.
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