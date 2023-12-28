using Cysharp.Threading.Tasks;
using Infrastructure.CommonSystems;
using MonoBehaviours;
using MonoBehaviours.Interfaces;
using UnityEngine;

namespace Infrastructure.Factories
{
    public interface ICommonSystemsFactory
    {
        UniTask InitializeCurtainLoadingAsync();
    }
}