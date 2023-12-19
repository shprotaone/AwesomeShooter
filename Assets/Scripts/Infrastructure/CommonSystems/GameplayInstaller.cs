using Infrastructure.ECS.Services;
using Infrastructure.ECS.Systems;
using Infrastructure.Factories;
using Leopotam.EcsLite;
using MonoBehaviours;
using UnityEngine;
using UnityEngine.Rendering;
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

            PlayerFactoryBinding();

            PlayerInitBinding();

            FooBinding();
        }

        private void FooBinding()
        {
            Container.Bind<Foo>().AsSingle();
            Container.Bind<IBar>().To<Bar>().AsSingle();
        }

        private void PlayerInitBinding()
        {
            Container.Bind<PlayerInitSystem>().AsSingle();
        }

        private void PlayerFactoryBinding()
        {
            Container.BindInterfacesAndSelfTo<PlayerFactory>().AsSingle();
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

    internal class Bar : IBar
    {
    }

    public class Foo
    {
        IBar _bar;

        public Foo(IBar bar)
        {
            _bar = bar;
            Debug.Log("Init");
        }
    }

    public interface IBar
    {

    }
}
