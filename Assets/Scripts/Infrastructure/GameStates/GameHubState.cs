using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagment;
using Infrastructure.GameStates;
using Infrastructure.SceneManagment;

namespace Infrastructure.Bootstrappers
{
    internal class GameHubState : IState
    {
        private ISceneLoader _sceneLoader;

        public GameHubState(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        public async UniTask Exit()
        {
            
        }

        public async UniTask Enter()
        {
            await _sceneLoader.Load(AssetAddress.MainMenuScenePath);
        }
    }
}