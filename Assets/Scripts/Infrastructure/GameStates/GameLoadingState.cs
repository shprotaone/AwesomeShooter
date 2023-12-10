using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagment;
using Infrastructure.CommonSystems;
using Scripts.Infrasructure;
using Zenject;

namespace Infrastructure.GameStates
{
    public class GameLoadingState : IState
    {
        private ILoadingCurtain _loadingCurtain;
        private ISceneLoader _sceneLoader;
        private IAssetProvider _assetProvider;
        
        public GameLoadingState(ILoadingCurtain loadingCurtain,
            ISceneLoader sceneLoader,
            IAssetProvider assetProvider)
        {
            _sceneLoader = sceneLoader;
            _assetProvider = assetProvider;
            _loadingCurtain = loadingCurtain;
        }

        public async UniTask Enter()
        {
            _loadingCurtain.Show();

            await _assetProvider.WarmupAssetsByLabel(AssetLabels.GameLoadingState);
            await _sceneLoader.Load(AssetAddress.MainMenuScenePath);
            
            _loadingCurtain.Hide();
        }

        public async UniTask Exit()
        {
            //_loadingCurtain.Show();
            await _assetProvider.ReleaseAssetsByLabel(AssetLabels.GameLoadingState);
        }
    }
}