using System;
using System.Collections;
using System.Collections.Generic;
using Infrastructure;
using Infrastructure.AssetManagment;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

public class GameRunner : MonoBehaviour
{
    private IInstantiator _instantiator;
    [SerializeField] private AssetReference _gameBootstrapper;
    private AsyncOperationHandle<GameObject> handle;

    [Inject]
    private void Construct(IInstantiator instantiator)
    {
        _instantiator = instantiator;
    }
    private void Awake()
    {
        var bootstrapper = FindObjectOfType<GameBootstrapper>();
        
        if(bootstrapper != null) return;

        StartCoroutine(LoadBootstrapper());
    }

    private IEnumerator LoadBootstrapper()
    {
        handle = Addressables.LoadAssetAsync<GameObject>(AssetAddress.GameBootstrapperPath);
        yield return handle;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            _instantiator.InstantiatePrefab(handle.Result);
        }
    }

    private void OnDestroy()
    {
        if(handle.IsValid())
            Addressables.Release(handle);
    }
}
