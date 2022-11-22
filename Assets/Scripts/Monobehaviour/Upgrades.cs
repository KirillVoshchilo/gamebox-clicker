using GameBoxClicker.AppEvents;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameBoxClicker
{
    public class Upgrades : MonoBehaviour, IStartNewGame
    {
        [SerializeField] private ContentSettings[] _contents;
        [SerializeField] private ScriptableContentSettingsEvent _onUpgradeContent;
        [SerializeField] private ScriptableEvent _onGameStart;
        [SerializeField] private PlayerResources _playerResources;
        [SerializeField] private ScriptableFloatEvent _onDecreaseResources;

        private Dictionary<ContentSettings, int> _contentCosts;
        public event Action OnCostChanged;

        public Dictionary<ContentSettings, int> ContentCosts => _contentCosts;

        private void Awake()
        {
            _onUpgradeContent.RegisterListener(Upgrade);
            _onGameStart.RegisterListener(StartNewGame);
        }
        private void OnDestroy()
        {
            _onUpgradeContent.UnregisterListener(Upgrade);
            _onGameStart.UnregisterListener(StartNewGame);
        }

        public void StartNewGame()
        {
            GetStartCosts();
        }

        private void GetStartCosts()
        {
            int count = _contents.Length;
            _contentCosts = new Dictionary<ContentSettings, int>();
            for (int k = 0; k < count; k++)
            {
                _contentCosts.Add(_contents[k], _contents[k].UpgradesLevel * (k + 1));
            }
            OnCostChanged?.Invoke();
        }
        private void Upgrade(ContentSettings contentSettings)
        {
            if (_contentCosts[contentSettings] < _playerResources.CurrentResources)
            {
                contentSettings.UpgradesLevel++;
                _onDecreaseResources.Raise(-_contentCosts[contentSettings]);
                int value = contentSettings.UpgradesLevel * 2;
                _contentCosts[contentSettings] = _contentCosts[contentSettings] * value;
                contentSettings.SetEarnPerClickMultiplier(value);
                contentSettings.SetIncomeMultiplier(value);
                OnCostChanged?.Invoke();
            }
        }
    }
}