using GameBoxClicker.AppEvents;
using TMPro;
using UnityEngine;

namespace GameBoxClicker.Canvases
{
    public class ResourcesOutput : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _tmpro;
        [SerializeField] private ScriptableFloatEvent _onResourcesChanged;
        [SerializeField] private ScriptableEvent _onStartNewGame;
        private string _outputFormat="0.#"; 

        private void Awake()
        {
            _onResourcesChanged.RegisterListener(ShowResources);
        }
        private void OnDestroy()
        {
            _onResourcesChanged.UnregisterListener(ShowResources);
        }

        private void ShowResources(float value)
        {
            _tmpro.text = value.ToString(_outputFormat);
        }
    }
}