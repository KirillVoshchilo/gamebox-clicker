using GameBoxClicker.AppEvents;
using TMPro;
using UnityEngine;

namespace GameBoxClicker.Canvases
{
    public class ResourcesOutput : MonoBehaviour, IStartEndGame
    {
        [SerializeField] private TextMeshProUGUI _tmpro;
        [SerializeField] private ScriptableIntEvent _onResourcesChanged;
        [SerializeField] private ScriptableEvent _onStartNewGame;
        [SerializeField] private ScriptableEvent _onEndGame;

        private void OnEnable()
        {
            _onResourcesChanged.RegisterListener(ShowResources);
            _onStartNewGame.RegisterListener(StartNewGame);
            _onEndGame.RegisterListener(EndGame);
        }
        private void OnDisable()
        {
            _onResourcesChanged.UnregisterListener(ShowResources);
            _onStartNewGame.UnregisterListener(StartNewGame);
            _onEndGame.UnregisterListener(EndGame);
        }

        public void EndGame()
        {
            _tmpro.text = "0";
        }
        public void StartNewGame()
        {

        }

        private void ShowResources(int value)
        {
            _tmpro.text = value.ToString();
        }
    }
}