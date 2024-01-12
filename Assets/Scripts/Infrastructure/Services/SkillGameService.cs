using Objects;
using UI;
using UnityEngine;

namespace Infrastructure.Services
{
    public class SkillGameService : ISkillGameService,IGameService
    {
        private UIService _uiService;
        private SkillChooseView _skillChooseView;
        
        private ILevelingGameService _levelingGameService;
        private PauseGameService _pauseGameService;

        public SkillGameService(UIService uiService,ILevelingGameService levelingGameService,
            PauseGameService pauseGameService,IServiceInitializer serviceInitializer)
        {
            _uiService = uiService;
            _levelingGameService = levelingGameService;
            _pauseGameService = pauseGameService;
            serviceInitializer.Add(this);
        }
        
        public void Init()
        {
            _skillChooseView = _uiService.GetView<SkillChooseView>();
            _levelingGameService.OnUpdateLevel += ShowSkillChooser;
        }

        private void ShowSkillChooser(int obj)
        {
            _skillChooseView.FillButtons(GetRandomSkills());
            _skillChooseView.Show();
            _pauseGameService.SetPaused(true);
        }

        private Skill[] GetRandomSkills()
        {
            Skill[] skills = new Skill[3];
            //TODO:RandomizeSkillLogic
            return skills;
        }
    }
}