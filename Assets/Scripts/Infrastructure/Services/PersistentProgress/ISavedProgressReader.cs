using GameBoxClicker.Infrastructure.Data;

namespace GameBoxClicker.Infrastructure.Services.PersistentProgress
{
    public interface ISavedProgressReader
    {
        public void LoadProgress(PlayerProgress progress);
    }
}