using Infrastructure.AssetManagment;
using Infrastructure.GameStates;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Infrastructure.Factories
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _startGame;

        private ISceneLoader _sceneLoader;
        [Inject]
        private void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        private void Start()
        {
            _startGame.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            _sceneLoader.Load(AssetAddress.GameplayScenePath);
        }
    }
}