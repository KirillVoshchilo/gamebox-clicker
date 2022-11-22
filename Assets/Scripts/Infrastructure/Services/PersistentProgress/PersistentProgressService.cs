using GameBoxClicker.Infrastructure.Data;

namespace GameBoxClicker.Infrastructure.Services.PersistentProgress
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public PlayerProgress Progress { get; set; }
    }
}