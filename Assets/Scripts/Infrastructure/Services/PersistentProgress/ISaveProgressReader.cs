using GameBoxClicker.Infrastructure.Data;

namespace GameBoxClicker.Infrastructure.Services.PersistentProgress
{
    public interface ISaveProgressReader
    {
        public void LoadProgress(PlayerProgress progress); 
    }
}