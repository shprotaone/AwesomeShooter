using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagment;
using Infrastructure.CommonSystems;
using Infrastructure.Factories;
using Scripts.Infrasructure;
using UnityEngine;

namespace Infrastructure.GameStates
{
    public class GameBootstrapState : IState
    {
        private GameStateMachine _gameStateMachine;
        private CommnonSystemsFactory _commnonSystemsFactory;
        private IAssetProvider _assetProvider;
        private StatesFactory _statesFactory;

        public GameBootstrapState(GameStateMachine gameStateMachine,
            CommnonSystemsFactory commonFactory,
            IAssetProvider assetProvider,
            StatesFactory statesFactory)
        {
            _assetProvider = assetProvider;
            _gameStateMachine = gameStateMachine;
            _commnonSystemsFactory = commonFactory;
            _statesFactory = statesFactory;

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