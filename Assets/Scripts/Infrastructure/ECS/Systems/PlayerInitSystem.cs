using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Extention;
using Infrastructure.CommonSystems;
using Infrastructure.ECS.Components;
using Infrastructure.ECS.Components.Tags;
using Infrastructure.Factories;
using Leopotam.EcsLite;
using MonoBehaviours.Interfaces;
using Settings;
using StaticData;
using UnityEngine;

public class PlayerInitSystem : IEcsInitSystem
{
    private EcsWorld _world;

    private readonly IPlayerFactory _playerFactory;
    private readonly ICommonSystemsFactory _commonSystemsFactory;
    private IGameSceneData _gameSceneData;
    private PlayerSettingsSO _playerSettingsSo;

    public PlayerInitSystem(IPlayerFactory playerFactory, 
        ICommonSystemsFactory commonSystemsFactory,
        PlayerSettingsSO playerSettings,
        ILevelSettingsLoader levelSettingsLoader)
    {
        _playerFactory = playerFactory;
        _commonSystemsFactory = commonSystemsFactory;
        _playerSettingsSo = playerSettings;
        _gameSceneData = levelSettingsLoader.GameSceneData;
    }

    public void Init(IEcsSystems systems)
    {
        _world = systems.GetWorld();
        CreateEntity(_gameSceneData);
    }

    private async void CreateEntity(IGameSceneData gameSceneData)
    {
        int entity = _world.NewEntity();
        GameObject playerPrefab = await _playerFactory.GetPlayer();
        GameObject player = GameObject.Instantiate(playerPrefab);
        await SetUpEntity(entity,player,_playerSettingsSo);
        player.transform.position = gameSceneData.SpawnPlayerPoint.position;
    }

    private async UniTask SetUpEntity(int entity,GameObject playerPrefab,PlayerSettingsSO settings)
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

        entity.ConstructEntity(_world,components);
    }
}