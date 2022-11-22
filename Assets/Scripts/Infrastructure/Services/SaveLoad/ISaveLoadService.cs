using GameBoxClicker.Infrastructure.Data;

namespace GameBoxClicker.Infrastructure.Services.SaveLoad
{
    public interface ISaveLoadService:IService
    {
        public void SaveProgress();
        public PlayerProgress LoadProgress();
    }
}