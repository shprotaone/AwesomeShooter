using Cysharp.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IServiceInitializer
    {
        UniTask Init();
        void Add(IGameService service);
    }
}