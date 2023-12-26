using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagment;
using Infrastructure.CommonSystems;
using Infrastructure.GameStates;
using Infrastructure.SceneManagment;
using Infrastructure.Services;

namespace Infrastructure.Bootstrappers
{
    internal class GameHubState : IState
    {
        private ILoadingCurtain _loadingCurtain;
        private ISceneLoader _sceneLoader;
        private CursorLockService _cursorLockService;

        public GameHubState(ISceneLoader sceneLoader,
            ILoadingCurtain loadingCurtain,
            CursorLockService cursorLockService)
        {
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _cursorLockService = cursorLockService;
        }

        public async UniTask Enter()
        {
            await _loadingCurtain.Show();
            await _sceneLoader.Load(AssetAddress.MainMenuScenePath);
            await _loadingCurtain.Hide();

            _cursorLockService.Show();
        }

        public async UniTask Exit()
        {
            
        }
    }
}