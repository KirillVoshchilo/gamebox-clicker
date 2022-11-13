using UnityEngine;
using TMPro;
using GameBoxClicker.AppEvents;

namespace GameBoxClicker.Canvases
{
    public class UpgradeContentButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _upgradeCostField;
        [SerializeField] private ScriptableContentSettingsEvent _onUpgradeContent;
        [SerializeField] private ContentSettings _contentSettings;
        [SerializeField] private Upgrades _upgrades;

        private void OnEnable()
        {
            UpdateOutput();
            _upgrades.OnCostChanged += UpdateOutput;
        }
        private void OnDisable()
        {
            _upgrades.OnCostChanged -= UpdateOutput;
        }

        public void Upgrade()
        {
            _onUpgradeContent.Raise(_contentSettings);
        }
        private void UpdateOutput()
        {
            _upgradeCostField.text = $"Lvl: {_contentSettings.UpgradesLevel} Cost: {_upgrades.ContentCosts[_contentSettings]}";
        }

    }
}