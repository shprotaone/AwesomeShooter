using Infrastructure.AssetManagment;
using Infrastructure.Bootstrappers;
using Infrastructure.GameStates;
using Infrastructure.SceneManagment;
using Infrastructure.StateMachines;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Infrastructure.Factories
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _startGame;
        private GameStateMachine _stateMachine;
        [Inject]
        private void Construct(ISceneLoader sceneLoader,GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        private void Start()
        {
            _startGame.onClick.AddListener(StartGame);
        }

        private async void StartGame()
        {
            await _stateMachine.Enter<GameplayState>();
            
        }
    }
}