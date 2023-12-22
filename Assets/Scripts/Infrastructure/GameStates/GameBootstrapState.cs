using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagment;
using Infrastructure.Factories;
using Infrastructure.StateMachines;

namespace Infrastructure.GameStates
{
    public class GameBootstrapState : IState
    {
        private GameStateMachine _gameStateMachine;
        private CommonSystemsFactory commonSystemsFactory;
        private IAssetProvider _assetProvider;

        public GameBootstrapState(GameStateMachine gameStateMachine,
            CommonSystemsFactory commonFactory,
            IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
            _gameStateMachine = gameStateMachine;
            commonSystemsFactory = commonFactory;
        }

        public async UniTask Enter()
        {
            await InitServices();
            _gameStateMachine.Enter<GameLoadingState>().Forget();
        }

        private async UniTask InitServices()
        {
            await _assetProvider.InitializeAsync();
            await commonSystemsFactory.InitializeCurtainLoadingAsync();
        }

        public UniTask Exit() => default;
    }
}