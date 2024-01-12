using Infrastructure.ECS.Systems;
using Zenject;

namespace Infrastructure.ECS
{
    public class ECSUpdateSystemsInstaller : Installer<ECSUpdateSystemsInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ExperienceSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInitSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyMovableSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<WeaponSpawnSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemySpawnSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<InputSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<MovementSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerJumpSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerMouseLookSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<GravitySystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerWeaponShootSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<ProjectileMovementSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<LifetimeSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<FireRateSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<ObstacleCollisionCheckSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyCollisionSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<ReloadMagazineSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<WeaponHolderSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<DamageSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<PeriodicDamageSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<DeathSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<EcsUpdateSystems>().AsSingle();

        }
    }
}