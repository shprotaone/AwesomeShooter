using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Extention;
using Infrastructure.CommonSystems;
using Infrastructure.ECS.Components;
using Infrastructure.ECS.Components.Tags;
using Infrastructure.ECS.Systems;
using Leopotam.EcsLite;
using MonoBehaviours.Interfaces;
using Settings;
using StaticData;
using UnityEngine;

public class PlayerInitSystem : IEcsInitSystem
{
    private EcsWorld _world;

    private readonly IPlayerFactory _playerFactory;
    private IGameSceneData _gameSceneData;

    public PlayerInitSystem(IPlayerFactory playerFactory, ILevelSettingsLoader levelSettingsLoader)
    {
        _playerFactory = playerFactory;
    }

    public void Init(IEcsSystems systems)
    {
        _world = systems.GetWorld();
        _gameSceneData = systems.GetShared<IGameSceneData>();
        CreateEntity(_gameSceneData);
    }

    private async UniTask CreateEntity(IGameSceneData gameSceneData)
    {
        GameObject playerPrefab = await _playerFactory.GetPlayer();
        GameObject player = GameObject.Instantiate(playerPrefab);

        player.transform.position = gameSceneData.SpawnPlayerPoint.position;
        var playerSettingsSo = gameSceneData.PlayerSettingsSo;
        var componentList = CreateComponents(player, playerSettingsSo);

        var entity = _world.NewEntityWithComponents(componentList);
        player.GetComponentInChildren<PlayerTagMono>().SetEntity(_world.PackEntity(entity));
    }

    private List<object> CreateComponents(GameObject playerPrefab,PlayerSettingsSO settings)
    {
        List<object> components = new List<object>();

        components.Add(new PlayerTag());
        components.Add(new DirectionComponent());

        components.Add(new ModelComponent()
        {
            modelTransform = playerPrefab.GetComponentInChildren<FaceTag>().transform
        });

        components.Add(new MovableComponent()
        {
            characterController = playerPrefab.GetComponent<CharacterController>(),
            speed = settings.Speed
        });

        components.Add(new MouseLookDirectionComponent()
        {
            camera = Camera.main,
            camExtension = playerPrefab.GetComponentInChildren<CinemachinePOVExtension>()
        });

        components.Add(new JumpComponent());
        components.Add(new GravityComponent()
        {
            gravity = WorldSettings.Gravity
        });

        components.Add(new GroundCheckComponent()
        {
            groundCheckTransform = playerPrefab.GetComponentInChildren<CheckGroundTag>().transform,
            groundDistance = settings.GroundDistance,
            groundMask = Masks.Floor
        });

        components.Add(new AroundScanComponent()
        {
            around = ""
        });

        components.Add(new WeaponHolderComponent()
        {
            holderTransform = playerPrefab.GetComponentInChildren<WeaponHolderTag>().transform,
            IsBusy = false
        });
        
        components.Add(new HealthComponent()
        {
            health = settings.Health
        });
        
        components.Add(new DamageComponent()
        {
            value = 5f
        });
        
        components.Add(new DeathComponent()
        {
            OnDeath = () => Debug.Log("Death")
        });
        
        components.Add(new ExperienceComponent()
        {
            Experience = 0,
            Level = 0
        });

        return components;
    }
}