using System.Collections.Generic;
using Infrastructure.Services;
using Objects;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class SkillChooseView : MonoBehaviour, IView
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private List<SkillCell> _skillCells;

        private PauseGameService _pauseGameService;
        
        [Inject]
        public void Construct(PauseGameService pauseGameService,ISkillGameService skillGameService)
        {
            _pauseGameService = pauseGameService;
            
        }
        
        public void FillButtons(Skill[] getRandomSkills)
        {
            for (int i = 0; i < _skillCells.Count; i++)
            {
                _skillCells[i].Fill(getRandomSkills[i]);
            }
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _closeButton.onClick.AddListener(Hide);
        }

        public void Hide()
        {
            _pauseGameService.SetPaused(false);
            gameObject.SetActive(false);
            _closeButton.onClick.RemoveListener(Hide);
        }
    }

}