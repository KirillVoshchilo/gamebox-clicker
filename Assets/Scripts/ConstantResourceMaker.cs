using GameBoxClicker.AppEvents;
using System.Collections;
using UnityEngine;

namespace GameBoxClicker
{
    public class ConstantResourceMaker : MonoBehaviour, IPauseHandler
    {
        [SerializeField] private int _earnPerTime;
        [SerializeField] private float _delayBetweenEarn;
        [SerializeField] private ScriptableIntEvent _onEarnResources;
        [SerializeField] private ScriptableEvent _onPauseGame;
        [SerializeField] private ScriptableEvent _onContinueGame;

        private WaitForSeconds _waiter;
        private Coroutine _earnProcessRoutine;

        private void Awake()
        {
            _waiter = new WaitForSeconds(_delayBetweenEarn);
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
            _onEarnResources.Raise(_earnPerTime);
            _earnProcessRoutine = StartCoroutine(EarnProcess());
        }
    }
}