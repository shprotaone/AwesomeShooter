using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Infrastructure.GameStates;

namespace Infrastructure.StateMachines
{
    public class StateMachine : IStateMachine
    {
        private readonly Dictionary<Type, IExitState> _registeredStates;
        private IExitState _currentState;

        public StateMachine() => _registeredStates = new Dictionary<Type, IExitState>();

        public async UniTask Enter<TState>() where TState : class, IState
        {
            TState newState = await ChangeState<TState>();
            await newState.Enter();
        }

        public async UniTask Exit<TState>() where TState : class, IState
        {
            //throw new System.NotImplementedException();
        }

        private async Task<TState> ChangeState<TState>() where TState : class, IExitState
        {
            if (_currentState != null)
                await _currentState.Exit();

            TState state = GetState<TState>();
            _currentState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitState
        {
            return _registeredStates[typeof(TState)] as TState;
        }

        public void RegisterState<TState>(TState state) where TState : IExitState
        {
            _registeredStates.Add(typeof(TState),state);
        }
    }
}