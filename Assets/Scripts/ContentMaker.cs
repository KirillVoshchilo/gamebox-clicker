using GameBoxClicker.AppEvents;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace GameBoxClicker
{
    public class ContentMaker : MonoBehaviour, IPauseHandler
    {
        [SerializeField] private AssetReference _creatingContent;
        [SerializeField] private int _maxContentCount;
        [SerializeField] private float _delayBetweenSpawn;
        [SerializeField] private ScriptableEvent _onPauseGame;
        [SerializeField] private ScriptableEvent _onContinueGame;

        [HideInInspector][SerializeField] private Field[] _fields;
        private WaitForSeconds _waiter;
        private int _currentContentCount;
        private Coroutine _spawnProcessRoutine;

        private void Awake()
        {
            _currentContentCount = 0;
            _waiter = new WaitForSeconds(_delayBetweenSpawn);
            _onPauseGame.RegisterListener(PauseGame);
            _onContinueGame.RegisterListener(ContinueGame);
            _spawnProcessRoutine = StartCoroutine(SpawnProcess());
        }
        private void OnDestroy()
        {
            _onPauseGame.UnregisterListener(PauseGame);
            _onContinueGame.UnregisterListener(ContinueGame);
        }

        public void PauseGame()
        {
            StopCoroutine(_spawnProcessRoutine);
        }
        public void ContinueGame()
        {
            _spawnProcessRoutine = StartCoroutine(SpawnProcess());
        }

        private IEnumerator SpawnProcess()
        {
            yield return _waiter;
            Task task = CreateContent();
            _spawnProcessRoutine = StartCoroutine(SpawnProcess());
        }
        private async Task CreateContent()
        {
            if (!CheckFofreeField(out List<int> freeFieldNumbers) || _currentContentCount >= _maxContentCount) return;
            int fieldNumber = freeFieldNumbers[Random.Range(0, freeFieldNumbers.Count)];
            _fields[fieldNumber].IsEmpty = false;
            _currentContentCount++;
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