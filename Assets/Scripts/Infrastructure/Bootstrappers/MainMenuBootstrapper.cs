using Infrastructure.CommonSystems;
using Infrastructure.Factories;
using UnityEngine;
using Zenject;

namespace Infrastructure.Bootstrappers
{
    public class MainMenuBootstrapper : MonoBehaviour
    {
        private UIFactory _uiFactory;
        private ILoadingCurtain _loadingCurtain;

        [Inject]
        public void Construct(UIFactory uiFactory,ILoadingCurtain loadingCurtain)
        {
            _uiFactory = uiFactory;
            _loadingCurtain = loadingCurtain;
        }
    
        public async void Start()
        {
            await _loadingCurtain.Show();
            await _uiFactory.CreateMainMenu();
            await _loadingCurtain.Hide();
        }
    }
}