using System.Collections;
using Infrastructure.AssetManagment;
using Infrastructure.Bootstrappers;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

namespace Infrastructure.CommonSystems
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private bool _loadFromMenu;
        private IInstantiator _instantiator;
        private AsyncOperationHandle<GameObject> handle;

        [Inject]
        private void Construct(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        private void Awake()
        {
            if (_loadFromMenu)
            {
                var bootstrapper = FindObjectOfType<GameBootstrapper>();

                if(bootstrapper != null) return;

                StartCoroutine(LoadBootstrapper());
            }
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
}
