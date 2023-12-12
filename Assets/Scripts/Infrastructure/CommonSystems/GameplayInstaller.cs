using Infrastructure.ECS;
using Infrastructure.ECS.Services;
using Leopotam.EcsLite;
using Zenject;

namespace Infrastructure.CommonSystems
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InputServiceBinding();
        }
        
        private void InputServiceBinding()
        {
            Container.Bind<InputService>().AsSingle();
        }
        
    }
}
