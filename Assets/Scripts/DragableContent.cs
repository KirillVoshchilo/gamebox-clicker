using GameBoxClicker.AppEvents;
using GameBoxClicker.Input;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace GameBoxClicker
{
    public class DragableContent : MonoBehaviour, IPointerDownHandler, IPauseHandler
    {
        [SerializeField] private float _movingSpeed;
        [SerializeField] private float _distanceBeforeDrag;
        [SerializeField] private ScriptableEvent _onPauseGame;
        [SerializeField] private ScriptableEvent _onContinueGame;
        [SerializeField] private UnityEvent OnDroped;

        private Vector3 _defaultPosition;
        private Vector3 _targetPosition;
        private Vector2 _positionOfStartClick;
        private Camera _camera;
        private Coroutine _calcTargetRoutine;
        private bool _isActive;

        private void Awake()
        {
            _defaultPosition = transform.position;
            _camera = Camera.main;
            _isActive = true;
            _onPauseGame.RegisterListener(PauseGame);
            _onContinueGame.RegisterListener(ContinueGame);
        }
        private void OnDestroy()
        {
            _onPauseGame.UnregisterListener(PauseGame);
            _onContinueGame.UnregisterListener(ContinueGame);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_isActive)
            {
                StartCoroutine(DragAndDropProcess());
                StartCoroutine(MovingProcess());
            }
        }
        public void PauseGame()
        {
            _isActive = false;
        }
        public void ContinueGame()
        {
            _isActive = true;
        }

        private void MoveToTarget()
        {
            if (_isActive)
                transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _movingSpeed * Time.deltaTime);
        }
        private void SetPointerPositionAsTarget()
        {
            Ray ray = _camera.ScreenPointToRay(PlayerInputHandler.PointerPosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);
            int count = hits.Length;
            for (int k = 0; k < count; k++)
            {
                if (hits[k].collider.gameObject.TryGetComponent(out Field field))
                {
                    _targetPosition = new Vector3(hits[k].point.x, transform.position.y, hits[k].point.z);
                    return;
                }
            }
            _targetPosition = transform.position;
            return;
        }
        private bool CheckForDrop()
        {
            return !PlayerInputHandler.HoldClick;
        }
        private bool CheckForTurnBack()
        {
            return _defaultPosition == transform.position;
        }
        private bool CheckForStartDragging()
        {
            if (PlayerInputHandler.HoldClick == false) StopAllCoroutines();
            float distance = (_positionOfStartClick - PlayerInputHandler.PointerPosition).magnitude;
            return _distanceBeforeDrag < distance;
        }
        private IEnumerator DragAndDropProcess()
        {
            _targetPosition = _defaultPosition;
            _positionOfStartClick = PlayerInputHandler.PointerPosition;
            WaitUntil waiter = new (CheckForStartDragging);
            yield return waiter;
            _calcTargetRoutine = StartCoroutine(CalcTergetPositionProcess());
            waiter = new WaitUntil(CheckForDrop);
            yield return waiter;
            OnDroped?.Invoke();
            StopCoroutine(_calcTargetRoutine);
            _targetPosition = _defaultPosition;
            waiter = new WaitUntil(CheckForTurnBack);
            yield return waiter;
            StopAllCoroutines();
        }
        private IEnumerator CalcTergetPositionProcess()
        {
            yield return null;
            SetPointerPositionAsTarget();
            _calcTargetRoutine = StartCoroutine(CalcTergetPositionProcess());
        }
        private IEnumerator MovingProcess()
        {
            yield return null;
            MoveToTarget();
            StartCoroutine(MovingProcess());
        }
    }
}