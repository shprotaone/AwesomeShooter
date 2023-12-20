using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagment;
using Infrastructure.CommonSystems;
using Infrastructure.ECS;
using Infrastructure.GameStates;
using Infrastructure.SceneManagment;
using UnityEngine;
using Zenject;

namespace Infrastructure.Bootstrappers
{
    public class GameplayState : IState
    {
        private IInstantiator _instantiator;
        private ISceneLoader _sceneLoader;


        public GameplayState(ISceneLoader sceneLoader,
            IInstantiator instantiator)
        {
            _sceneLoader = sceneLoader;
            _instantiator = instantiator;
        }

        public async UniTask Enter()
        {
            await _sceneLoader.Load(AssetAddress.GameplayScenePath);
        }

        public async UniTask Exit()
        {
            Debug.Log("Exit "+ this.GetType());
        }
    }
}