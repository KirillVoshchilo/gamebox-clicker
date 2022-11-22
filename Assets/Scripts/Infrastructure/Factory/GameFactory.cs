using GameBoxClicker.Infrastructure.AssetManagement;
using GameBoxClicker.Infrastructure.Services.PersistentProgress;
using System.Collections.Generic;
using UnityEngine;

namespace GameBoxClicker.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;

        public GameFactory(IAssetProvider assets)
        {
            _assets = assets;
        }

        public List<ISaveProgressReader> ProgressReaders => throw new System.NotImplementedException();

        public List<ISavedProgress> ProgressWriters => throw new System.NotImplementedException();

        public void Cleanup()
        {
            throw new System.NotImplementedException();
        }

        public GameObject CreateContent(GameObject at)
        {
            throw new System.NotImplementedException();
        }
    }
}