using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagment;
using Infrastructure.GameStates;
using UnityEngine;

namespace Infrastructure
{
    internal class GameplayState : IState
    {
        private ISceneLoader _sceneLoader;
        
        public GameplayState(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        public async UniTask Exit()
        {
            Debug.Log("Exit "+ this.GetType());
        }

        public async UniTask Enter()
        {
            await _sceneLoader.Load(AssetAddress.GameplayScenePath);
        }
    }
}