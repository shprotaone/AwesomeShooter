using Infrastructure.ECS.Systems;
using UnityEngine.Rendering;
using Zenject;

namespace Infrastructure.ECS
{
    public class ECSUpdateSystemsInstaller : Installer<ECSUpdateSystemsInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerInitSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<InputSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<MovementSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerJumpSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerMouseLookSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<CursorLockSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<GravitySystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerWeaponShootSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<LifetimeSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<FireRateSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<ObstacleCollisionCheckSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyCollisionCheckSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<ReloadMagazineSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<WeaponHolderSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<PickUpSystem>().AsSingle();

            Container.BindInterfacesAndSelfTo<EcsUpdateSystems>().AsSingle();

        }
    }
}