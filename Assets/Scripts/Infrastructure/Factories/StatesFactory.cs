using Infrastructure.GameStates;
using Zenject;

namespace Infrastructure.Factories
{
    public class StatesFactory
    {
        //автоиньектор в контейнер с созданием объекта. Используется только для nonBeh
        
        private IInstantiator _instantiator;

        public StatesFactory(IInstantiator instantiator) => 
            _instantiator = instantiator;

        public TState Create<TState>() where TState : IExitState => 
            _instantiator.Instantiate<TState>();
    }
}