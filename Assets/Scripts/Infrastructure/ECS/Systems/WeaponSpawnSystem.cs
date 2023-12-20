using Infrastructure.CommonSystems;
using Leopotam.EcsLite;
using MonoBehaviours.Interfaces;
using UnityEngine;

namespace Infrastructure.ECS.Systems
{
    public class WeaponSpawnSystem : IEcsInitSystem
    {
        private EcsWorld _ecsWorld;
        private IWeaponFactory _weaponFactory;
        private IGameSceneData _gameSceneData;
        private ILevelSettingsLoader _levelSettingsLoader;

        public WeaponSpawnSystem(IWeaponFactory weaponFactory,
            ILevelSettingsLoader levelSettingsLoader)
        {
            _weaponFactory = weaponFactory;
            _gameSceneData = levelSettingsLoader.GameSceneData;
        }
        
        public void Init(IEcsSystems systems)
        {
            _ecsWorld = systems.GetWorld();
            //по количеству точек
            foreach (Transform point in _gameSceneData.SpawnWeaponPoints)
            {
                SpawnWeapon(point);
            }
            
        }

        private async void SpawnWeapon(Transform point)
        {
            int entity = _ecsWorld.NewEntity();
        }
    }
}
