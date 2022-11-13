using GameBoxClicker.AppEvents;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameBoxClicker
{
    public class ClickResourceMaker : MonoBehaviour, IPointerClickHandler, IPauseHandler
    {
        [SerializeField] private ContentSettings _contentSettings;
        [SerializeField] private ScriptableFloatEvent _onEarnResources;
        [SerializeField] private ScriptableEvent _onPauseGame;
        [SerializeField] private ScriptableEvent _onContinueGame;
        private bool _isActive;

        private void Awake()
        {
            _isActive = true;
            _onPauseGame.RegisterListener(PauseGame);
            _onContinueGame.RegisterListener(ContinueGame);
        }
        private void OnDestroy()
        {
            _onPauseGame.UnregisterListener(PauseGame);
            _onContinueGame.UnregisterListener(ContinueGame);
        }

        public void ContinueGame()
        {
            _isActive = true;
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if (_isActive) _onEarnResources.Raise(_contentSettings.EarnPerClick);
        }
        public void PauseGame()
        {
            _isActive = false;
        }
    }
}