using Cysharp.Threading.Tasks;
using Objects;
using Settings;

namespace Infrastructure.Factories
{
    public interface IEnemyFactory
    {
        UniTask<Enemy> GetEnemy(EnemyType type);
    }
}