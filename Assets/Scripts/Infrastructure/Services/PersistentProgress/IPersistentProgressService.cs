using GameBoxClicker.Infrastructure.Data;

namespace GameBoxClicker.Infrastructure.Services.PersistentProgress
{
    public interface IPersistentProgressService:IService
    {
        PlayerProgress Progress { get; set; }
    }
}