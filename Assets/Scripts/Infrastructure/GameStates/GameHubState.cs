using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagment;
using Infrastructure.CommonSystems;
using Infrastructure.GameStates;
using Infrastructure.SceneManagment;

namespace Infrastructure.Bootstrappers
{
    internal class GameHubState : IState
    {
        private ILoadingCurtain _loadingCurtain;
        private ISceneLoader _sceneLoader;

        public GameHubState(ISceneLoader sceneLoader,ILoadingCurtain loadingCurtain)
        {
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
        }
        public async UniTask Exit()
        {
            
        }

        public async UniTask Enter()
        {
            await _loadingCurtain.Show();
            await _sceneLoader.Load(AssetAddress.MainMenuScenePath);
            await _loadingCurtain.Hide();
        }
    }
}