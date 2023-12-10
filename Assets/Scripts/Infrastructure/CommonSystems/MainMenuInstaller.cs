using Zenject;

namespace Infrastructure.CommonSystems
{
    public class MainMenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindingUIFactory();

            CreateMainMenu();
            
        }

        private void BindingUIFactory()
        {
            Container.Bind<UIFactory>().AsSingle();
        }

        private void CreateMainMenu()
        {
            Container.Bind<MainMenuBootstrapper>().AsSingle();
        }
    }
}