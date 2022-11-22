using GameBoxClicker.Infrastructure.Services;
using GameBoxClicker.Infrastructure.Services.PersistentProgress;
using System.Collections.Generic;
using UnityEngine;

namespace GameBoxClicker.Infrastructure.Factory
{
    public interface IGameFactory: IService
    {
        public void Cleanup();
        GameObject CreateContent(GameObject at);
        List<ISaveProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
    }
}