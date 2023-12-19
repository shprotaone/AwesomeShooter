using Cysharp.Threading.Tasks;
using Leopotam.EcsLite;
using MonoBehaviours.Interfaces;
using Settings.Weapons;
using UnityEngine;

namespace Infrastructure.ECS.Systems
{
    public class WeaponInitSystem : IEcsInitSystem
    {
        private EcsWorld _ecsWorld;
        private IWeaponFactory _weaponFactory;
        private IGameSceneData _gameSceneData;

        public WeaponInitSystem(IWeaponFactory weaponFactory)
        {
            _weaponFactory = weaponFactory;
            
        }
        
        public void Init(IEcsSystems systems)
        {
            _ecsWorld = systems.GetWorld();
        }
    }

    public interface IWeaponFactory
    {
        UniTask<GameObject> GetWeapon(WeaponType weaponType);
    }
}
