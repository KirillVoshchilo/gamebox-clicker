using GameBoxClicker.Infrastructure.Data;
using GameBoxClicker.Infrastructure.Factory;
using GameBoxClicker.Infrastructure.Services.PersistentProgress;

namespace GameBoxClicker.Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string PROGRESS_KEY = "Progress";
        private readonly IPersistentProgressService _progressService;
        private readonly IGameFactory _gameFactory;
        public SaveLoadService(IPersistentProgressService progressService, IGameFactory gameFactory)
        {
            _progressService = progressService;
            _gameFactory = gameFactory;
        }

        public PlayerProgress LoadProgress()
        {
            throw new System.NotImplementedException();
        }

        public void SaveProgress()
        {
            throw new System.NotImplementedException();
        }
    }
}