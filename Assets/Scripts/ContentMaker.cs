using GameBoxClicker.AppEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBoxClicker
{
    public class ContentMaker : MonoBehaviour, IPauseHandler, IStartNewGame, IEndGame
    {
        [SerializeField] private MergeContent[] _levelMergeContent;
        [SerializeField] private int _maxContentCount;
        [SerializeField] private float _delayBetweenSpawn;
        [SerializeField] private ScriptableEvent _onPauseGame;
        [SerializeField] private ScriptableEvent _onContinueGame;
        [SerializeField] private ScriptableEvent _onStartNewGame;
        [SerializeField] private ScriptableEvent _onEndGame;

        [HideInInspector][SerializeField] private Field[] _fields;
        private WaitForSeconds _waiter;
        private Coroutine _spawnProcessRoutine;

        private void Awake()
        {
            _onStartNewGame.RegisterListener(StartNewGame);
            _onEndGame.RegisterListener(EndGame);
        }
        private void OnDestroy()
        {
            _onStartNewGame.UnregisterListener(StartNewGame);
            _onEndGame.UnregisterListener(EndGame);
        }
        public void PauseGame()
        {
            StopCoroutine(_spawnProcessRoutine);
        }
        public void ContinueGame()
        {
            _spawnProcessRoutine = StartCoroutine(SpawnProcess());
        }
        public void StartNewGame()
        {
            int count = _fields.Length;
            for (int k = 0; k < count; k++)
            {
                _fields[k].ClearTheField();
            }
            _waiter = new WaitForSeconds(_delayBetweenSpawn);
            _onPauseGame.RegisterListener(PauseGame);
            _onContinueGame.RegisterListener(ContinueGame);
            StartSpawn();
        }
        public void EndGame()
        {
            StopAllCoroutines();
            int count = _fields.Length;
            for (int k = 0; k < count; k++)
            {
                if (_fields[k].CurrentContent != null) Destroy(_fields[k].CurrentContent.gameObject);
                _fields[k].ClearTheField();
            }
            _onPauseGame.UnregisterListener(PauseGame);
            _onContinueGame.UnregisterListener(ContinueGame);
        }

        private void StartSpawn()
        {
            CreateContent();
            _spawnProcessRoutine = StartCoroutine(SpawnProcess());
        }
        private IEnumerator SpawnProcess()
        {
            yield return _waiter;
            CreateContent();
            _spawnProcessRoutine = StartCoroutine(SpawnProcess());
        }
        private void CreateContent()
        {
            bool isThereFreeField = CheckFofreeField(out List<int> freeFieldNumbers);
            if (!isThereFreeField || _fields.Length - freeFieldNumbers.Count >= _maxContentCount) return;
            int fieldNumber = freeFieldNumbers[UnityEngine.Random.Range(0, freeFieldNumbers.Count)];
            _fields[fieldNumber].IsEmpty = false;
            Transform targetTransform = _fields[fieldNumber].SpawnTransform;
            GameObject gameObject = Instantiate(_levelMergeContent[0].gameObject, targetTransform.position, targetTransform.rotation, targetTransform);
            MergeContent content = gameObject.GetComponent<MergeContent>();
            _fields[fieldNumber].CurrentContent = content;
            content.Field = _fields[fieldNumber];
        }
        private bool CheckFofreeField(out List<int> freeFieldNumbers)
        {
            freeFieldNumbers = new List<int>();
            int count = _fields.Length;
            for (int k = 0; k < count; k++)
            {
                if (_fields[k].IsEmpty) freeFieldNumbers.Add(k);
            }
            return freeFieldNumbers.Count != 0;
        }

        [ContextMenu("???????? ???????????? ????")]
        private void GetFields()
        {
            _fields = GetComponentsInChildren<Field>();
        }
    }
}