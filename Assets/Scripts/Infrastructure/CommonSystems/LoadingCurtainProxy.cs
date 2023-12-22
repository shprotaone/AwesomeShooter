using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Infrastructure.CommonSystems
{
    public class LoadingCurtainProxy : ILoadingCurtain
    {
        private ILoadingCurtain _curtain;

        public async UniTask InitializeCurtain(ILoadingCurtain curtain)
        {
            _curtain = curtain;
        }
        public async UniTask Show()
        {
            _curtain.Show();
        }

        public async UniTask Hide()
        {
            _curtain.Hide();
        }
    }
}