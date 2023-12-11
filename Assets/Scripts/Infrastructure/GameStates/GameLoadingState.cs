using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagment;
using Infrastructure.Bootstrappers;
using Infrastructure.CommonSystems;
using Scripts.Infrasructure;
using UnityEngine.UIElements;
using Zenject;

namespace Infrastructure.GameStates
{
    public class GameLoadingState : IState
    {
        private ILoadingCurtain _loadingCurtain;
        private ISceneLoader _sceneLoader;
        private IAssetProvider _assetProvider;
        private GameStateMachine _stateMachine;
        
        public GameLoadingState(ILoadingCurtain loadingCurtain,
            ISceneLoader sceneLoader,
            IAssetProvider assetProvider,
            GameStateMachine stateMachine)
        {
            _sceneLoader = sceneLoader;
            _assetProvider = assetProvider;
            _loadingCurtain = loadingCurtain;
            _stateMachine = stateMachine;
        }

        public async UniTask Enter()
        {
            _loadingCurtain.Show();

            await _assetProvider.WarmupAssetsByLabel(AssetLabels.GameLoadingState);
            _loadingCurtain.Hide();

            await _stateMachine.Enter<GameHubState>();

        }

        public async UniTask Exit()
        {
            await _assetProvider.ReleaseAssetsByLabel(AssetLabels.GameLoadingState);
        }
    }
}