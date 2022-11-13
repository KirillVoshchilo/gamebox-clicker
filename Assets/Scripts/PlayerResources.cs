using GameBoxClicker.AppEvents;
using UnityEngine;

namespace GameBoxClicker
{
    public class PlayerResources : MonoBehaviour, IStartNewGame
    {
        [SerializeField] private ScriptableFloatEvent _onEarnResources;
        [SerializeField] private ScriptableFloatEvent _onResourcesChanged;
        [SerializeField] private ScriptableEvent _onStartNewGame;
        [SerializeField] private ScriptableFloatEvent _onLastMergeEarn;
        private float _currentResources;

        public float CurrentResources
        {
            get
            {
                return _currentResources;
            }
            set
            {
                _currentResources = value;
                if (_onResourcesChanged!=null) _onResourcesChanged.Raise(_currentResources);
            }
        }

        private void Awake()
        {
            _onLastMergeEarn.RegisterListener(AddResources);
            _onEarnResources.RegisterListener(AddResources);
            _onStartNewGame.RegisterListener(StartNewGame);
        }
        private void OnDestroy()
        {
            _onEarnResources.UnregisterListener(AddResources);
            _onStartNewGame.UnregisterListener(StartNewGame);
            _onLastMergeEarn.UnregisterListener(AddResources);
        }

        public void StartNewGame()
        {
            CurrentResources = 0;
        }

        private void AddResources(float value)
        {
            CurrentResources += value;
        }
    }
}