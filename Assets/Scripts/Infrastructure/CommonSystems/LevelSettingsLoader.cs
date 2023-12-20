using Cysharp.Threading.Tasks;
using Infrastructure.Factories;
using MonoBehaviours;
using MonoBehaviours.Interfaces;

namespace Infrastructure.CommonSystems
{
    public class LevelSettingsLoader : ILevelSettingsLoader
    {
        private IGameSceneData _gameSceneData;
        private ICommonSystemsFactory _commonSystemsFactory;

        public IGameSceneData GameSceneData => _gameSceneData;

        public LevelSettingsLoader(ICommonSystemsFactory commonSystemsFactory)
        {
            _commonSystemsFactory = commonSystemsFactory;
        }

        public async UniTask LoadGameSceneData()
        {
            _gameSceneData = await _commonSystemsFactory.GetGameSceneData();
        }
    }
}