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
        private ILoadingCurtain _loadingCurtain;
        private IInstantiator _instantiator;
        private ISceneLoader _sceneLoader;


        public GameplayState(ISceneLoader sceneLoader,
            IInstantiator instantiator,
            ILoadingCurtain loadingCurtain)
        {
            _sceneLoader = sceneLoader;
            _instantiator = instantiator;
            _loadingCurtain = loadingCurtain;
        }

        public async UniTask Enter()
        {
            await _loadingCurtain.Show();
            await _sceneLoader.Load(AssetAddress.GameplayScenePath);
            await _loadingCurtain.Hide();
        }

        public async UniTask Exit()
        {
            Debug.Log("Exit "+ this.GetType());
        }
    }
}