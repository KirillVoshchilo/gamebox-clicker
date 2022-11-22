using GameBoxClicker.AppEvents;
using System.Collections;
using UnityEngine;

namespace GameBoxClicker
{
    public class ConstantResourceMaker : MonoBehaviour, IPauseHandler
    {
        [SerializeField] private ContentSettings _contentSettings;
        [SerializeField] private ScriptableFloatEvent _onEarnResources;
        [SerializeField] private ScriptableEvent _onPauseGame;
        [SerializeField] private ScriptableEvent _onContinueGame;

        private WaitForSeconds _waiter;
        private Coroutine _earnProcessRoutine;

        private void Awake()
        {
            _waiter = new WaitForSeconds(_contentSettings.DelayBetweenIncome);
            _earnProcessRoutine = StartCoroutine(EarnProcess());
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
            _earnProcessRoutine = StartCoroutine(EarnProcess());
        }
        public void PauseGame()
        {
            StopCoroutine(_earnProcessRoutine);
        }

        private IEnumerator EarnProcess()
        {
            yield return _waiter;
            _onEarnResources.Raise(_contentSettings.Income);
            _earnProcessRoutine = StartCoroutine(EarnProcess());
        }
    }
}