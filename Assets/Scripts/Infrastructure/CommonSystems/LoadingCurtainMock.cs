using Cysharp.Threading.Tasks;
using Infrastructure.CommonSystems;
using UnityEngine;

internal class LoadingCurtainMock : ILoadingCurtain
{
    public async UniTask Show()
    {
        Debug.Log("ShowMock");
    }

    public async UniTask Hide()
    {
       
    }
}