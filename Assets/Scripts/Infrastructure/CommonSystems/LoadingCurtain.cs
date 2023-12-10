using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Infrastructure.CommonSystems
{
    public class LoadingCurtain : MonoBehaviour,ILoadingCurtain
    {
        [SerializeField] private float _duration = 1;
        [SerializeField] private CanvasGroup _canvasGroup;
        
        public async UniTask Show()
        {
            await _canvasGroup.DOFade(1, 1).Play().ToUniTask();
            Debug.Log("Show");
        }

        public async UniTask Hide()
        {
            await _canvasGroup.DOFade(0, 1).ToUniTask();
        }
    }
}