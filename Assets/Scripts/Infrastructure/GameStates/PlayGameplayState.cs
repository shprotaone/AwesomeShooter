using System;
using Cysharp.Threading.Tasks;
using Infrastructure.GameStates;
using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.Bootstrappers
{
    public class PlayGameplayState : IState
    {
        private CursorLockService _cursorLockService;

        public PlayGameplayState(CursorLockService cursorLockService)
        {
            _cursorLockService = cursorLockService;
        }

        public async UniTask Enter()
        {
            Debug.Log("GameStarted");
            _cursorLockService.Hide();
        }

        public async UniTask Exit()
        {
            
        }
    }
}