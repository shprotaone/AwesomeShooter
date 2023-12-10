using Cysharp.Threading.Tasks;

namespace Infrastructure.CommonSystems
{
    public interface ILoadingCurtain
    {
        UniTask Show();
        UniTask Hide();
    }
}