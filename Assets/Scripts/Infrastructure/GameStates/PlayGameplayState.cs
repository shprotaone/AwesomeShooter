using System;
using Cysharp.Threading.Tasks;
using Infrastructure.GameStates;
using UnityEngine;

namespace Infrastructure.Bootstrappers
{
    public class PlayGameplayState : IState
    {
        public async UniTask Enter()
        {
            Debug.Log("GameStarted");
        }

        public async UniTask Exit()
        {
            
        }
    }
}