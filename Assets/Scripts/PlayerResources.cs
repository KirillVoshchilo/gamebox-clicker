using GameBoxClicker.AppEvents;
using UnityEngine;

namespace GameBoxClicker
{
    public class PlayerResources : MonoBehaviour
    {
        [SerializeField] private ScriptableIntEvent _onEarnResources;
        [SerializeField] private ScriptableIntEvent _onResourcesChanged;
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
            _onEarnResources.RegisterListener(AddResources);
        }
        private void OnDestroy()
        {
            _onEarnResources.UnregisterListener(AddResources);
        }

        private void AddResources(int value)
        {
            CurrentResources += value;
        }
    }
}