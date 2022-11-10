using GameBoxClicker.AppEvents;
using UnityEngine;

namespace GameBoxClicker
{
    public class PlayerResources : MonoBehaviour, IStartEndGame
    {
        [SerializeField] private ScriptableIntEvent _onEarnResources;
        [SerializeField] private ScriptableIntEvent _onResourcesChanged;
        [SerializeField] private ScriptableEvent _onStartNewGame;
        [SerializeField] private ScriptableEvent _onEndGame;
        [SerializeField] private ScriptableIntEvent _onLastMergeEarn;
        private int _currentResources;

        public int CurrentResources
        {
            get
            {
                return _currentResources;
            }
            set
            {
                _currentResources = value;
                _onResourcesChanged?.Raise(_currentResources);
            }
        }

        private void Awake()
        {
            _onLastMergeEarn.RegisterListener(AddResources);
            _onEarnResources.RegisterListener(AddResources);
            _onStartNewGame.RegisterListener(StartNewGame);
            _onEndGame.RegisterListener(EndGame);
        }
        private void OnDestroy()
        {
            _onEarnResources.UnregisterListener(AddResources);
            _onStartNewGame.UnregisterListener(StartNewGame);
            _onEndGame.UnregisterListener(EndGame);
            _onLastMergeEarn.UnregisterListener(AddResources);
        }

        private void AddResources(int value)
        {
            CurrentResources += value;
        }

        public void StartNewGame()
        {
            CurrentResources = 0;
        }

        public void EndGame()
        {
            CurrentResources = 0;
        }
    }
}