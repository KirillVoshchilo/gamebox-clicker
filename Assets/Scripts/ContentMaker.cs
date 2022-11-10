using GameBoxClicker.AppEvents;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace GameBoxClicker
{
    public class ContentMaker : MonoBehaviour, IPauseHandler, IStartEndGame
    {
        [SerializeField] private AssetReference _creatingContent;
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
            _spawnProcessRoutine = StartCoroutine(SpawnProcess());
        }
        public void EndGame()
        {
            StopAllCoroutines();
            int count = _fields.Length;
            for (int k = 0; k < count; k++)
            {
                _fields[k].ClearTheField();
            }
            _onPauseGame.UnregisterListener(PauseGame);
            _onContinueGame.UnregisterListener(ContinueGame);
        }

        private IEnumerator SpawnProcess()
        {
            yield return _waiter;
            Task task = CreateContent();
            _spawnProcessRoutine = StartCoroutine(SpawnProcess());
        }
        private async Task CreateContent()
        {
           bool isThereFreeField= CheckFofreeField(out List<int> freeFieldNumbers);
            if (!isThereFreeField || _fields.Length- freeFieldNumbers.Count >= _maxContentCount) return;
            int fieldNumber = freeFieldNumbers[Random.Range(0, freeFieldNumbers.Count)];
            _fields[fieldNumber].IsEmpty = false;
            Transform targetTransform = _fields[fieldNumber].SpawnTransform;
            AsyncOperationHandle<GameObject> handle = Addressables
                .InstantiateAsync(_creatingContent, targetTransform.position, targetTransform.rotation, targetTransform);
            await handle.Task;
            MergeContent content = handle.Result.GetComponent<MergeContent>();
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

        [ContextMenu("Обновить используемые поля")]
        private void GetFields()
        {
            _fields = GetComponentsInChildren<Field>();
        }


    }
}