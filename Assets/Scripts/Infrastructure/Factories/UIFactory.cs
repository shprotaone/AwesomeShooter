using System;
using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagment;
using Infrastructure.Factories;
using UnityEngine;
using Zenject;

public class UIFactory
{
    private IAssetProvider _assetProvider;
    private IInstantiator _instantiator;

    [Inject]
    public UIFactory(IAssetProvider assetProvider, 
        IInstantiator instantiator)
    {
        _assetProvider = assetProvider;
        _instantiator = instantiator;
    }

    public async UniTask<MainMenu> CreateMainMenu()
    {
        GameObject prefab = await _assetProvider.Load<GameObject>(AssetAddress.MainMenuCanvasPath);
        GameObject newPrefab = _instantiator.InstantiatePrefab(prefab);
        return newPrefab.GetComponent<MainMenu>();
    }
}