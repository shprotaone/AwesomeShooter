using System;
using Infrastructure.Services;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI
{
    public class LevelInfoView : MonoBehaviour,IView
    {
        [SerializeField] private TMP_Text _leftEnemiesText;

        private ILevelProgressService _levelProgressService;
        
        [Inject]
        public void Construct(ILevelProgressService levelProgressService)
        {
            _levelProgressService = levelProgressService;
            levelProgressService.OnLeftEnemy += UpdateText;
        }

        private void UpdateText(int count)
        {
            _leftEnemiesText.text = count.ToString();
        }

        private void OnDestroy()
        {
            _levelProgressService.OnLeftEnemy -= UpdateText;
        }
    }
}