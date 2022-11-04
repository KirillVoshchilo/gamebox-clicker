using GameBoxClicker.AppEvents;
using TMPro;
using UnityEngine;

namespace GameBoxClicker.Canvases
{
    public class ResourcesOutput : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _tmpro;
        [SerializeField] private ScriptableIntEvent _onResourcesChanged;

        private void Awake()
        {
            _onResourcesChanged.RegisterListener(ShowResources);
        }
        private void OnDestroy()
        {
            _onResourcesChanged.UnregisterListener(ShowResources);
        }

        private void ShowResources(int value)
        {
            _tmpro.text = value.ToString();
        }
    }
}