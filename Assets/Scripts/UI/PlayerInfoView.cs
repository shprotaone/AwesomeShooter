using System;
using Infrastructure.CommonSystems;
using Infrastructure.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class PlayerInfoView : MonoBehaviour,IView
    {
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private Slider _hpSlider;
        [SerializeField] private Slider _expSLider;

        private LevelSettingsContainer _levelSettingsSo;
        private LevelingGameService _levelingGameService;
        
        [Inject]
        private void Construct(LevelSettingsContainer levelSettingsSo,
            LevelingGameService levelingGameService)
        {
            _levelSettingsSo = levelSettingsSo;
            _levelingGameService = levelingGameService;
             _levelingGameService.OnUpdateLevel += UpdateView;
             _levelingGameService.OnUpdateLevel += SetUpExp;
        }

        private void UpdateView(int obj)
        {
            _levelText.text = $"{obj} lvl";
        }

        public void SetUpHP(float hp)
        {
            _hpSlider.maxValue = hp;
            _hpSlider.value = hp;
        }

        public void SetUpExp(int currentLevel)
        {
            _expSLider.minValue = _expSLider.value;
            
            _expSLider.maxValue = _levelSettingsSo.PlayerLevelProgress.levels.
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

        private void OnDisable()
        {
            _levelingGameService.OnUpdateLevel -= UpdateView;
            _levelingGameService.OnUpdateLevel -= SetUpExp;
        }
    }
}