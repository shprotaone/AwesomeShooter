using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagment;
using Infrastructure.Factories;
using Infrastructure.StateMachines;

namespace Infrastructure.GameStates
{
    public class GameBootstrapState : IState
    {
        private GameStateMachine _gameStateMachine;
        private CommnonSystemsFactory _commnonSystemsFactory;
        private IAssetProvider _assetProvider;

        public GameBootstrapState(GameStateMachine gameStateMachine,
            CommnonSystemsFactory commonFactory,
            IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
            _gameStateMachine = gameStateMachine;
            _commnonSystemsFactory = commonFactory;
        }

        public async UniTask Enter()
        {
            await InitServices();
            _gameStateMachine.Enter<GameLoadingState>().Forget();
        }

        private async UniTask InitServices()
        {
            await _assetProvider.InitializeAsync();
            await _commnonSystemsFactory.InitializeCurtainLoadingAsync();
        }

        public UniTask Exit() => default;
    }
}