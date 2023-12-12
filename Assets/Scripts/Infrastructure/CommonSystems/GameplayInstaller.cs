using Infrastructure.ECS.Services;
using Infrastructure.ECS.Systems;
using Zenject;

namespace Infrastructure.CommonSystems
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InputServiceBinding();

            BulletFactoryBinding();

            BulletPoolBinding();
        }

        private void BulletPoolBinding()
        {
            Container.Bind<BulletPool>().AsSingle();
        }

        private void BulletFactoryBinding()
        {
            Container.Bind<BulletFactory>().AsSingle();
        }

        private void InputServiceBinding()
        {
            Container.Bind<InputService>().AsSingle();
        }
        
    }
}
