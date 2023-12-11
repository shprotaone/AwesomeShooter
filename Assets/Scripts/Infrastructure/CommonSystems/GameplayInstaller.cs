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

            //BindECSSystems();
        }

        private void BindECSSystems()
        {
            BindSystem<InputSystem>();
        }

        private void InputServiceBinding()
        {
            Container.Bind<InputService>().AsSingle();
        }
        
        private void BindSystem<TSystem>() where TSystem : IEcsSystem
        {
            Container.Bind<IEcsSystem>().To<TSystem>().AsTransient();
        }
    }
}
