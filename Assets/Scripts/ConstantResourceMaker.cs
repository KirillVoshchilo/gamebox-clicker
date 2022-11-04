using GameBoxClicker.AppEvents;
using System.Collections;
using UnityEngine;

namespace GameBoxClicker
{
    public class ConstantResourceMaker : MonoBehaviour
    {
        [SerializeField] private int _earnPerTime;
        [SerializeField] private float _delayBetweenEarn;
        [SerializeField] private ScriptableIntEvent _onEarnResources;

        private WaitForSeconds _waiter;

        private void Awake()
        {
            _waiter = new WaitForSeconds(_delayBetweenEarn);
            StartCoroutine(EarnProcess());
        }
        private IEnumerator EarnProcess()
        {
            yield return _waiter;
            _onEarnResources.Raise(_earnPerTime);
            StartCoroutine(EarnProcess());
        }
    }
}