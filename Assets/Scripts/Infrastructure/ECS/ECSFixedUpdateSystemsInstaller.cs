using Infrastructure.ECS.Systems;
using Zenject;

namespace Infrastructure.ECS
{
    public class ECSFixedUpdateSystemsInstaller : Installer<ECSFixedUpdateSystemsInstaller>
    {
        public override void InstallBindings()
        {          
            Container.BindInterfacesAndSelfTo<PlayerGroundCheckSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<PickUpSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnActivatorSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<EcsFixedSystems>().AsSingle();
        }
    }
}