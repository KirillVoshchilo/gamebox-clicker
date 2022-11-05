using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameBoxClicker
{
    public class DragableContent : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private float _movingSpeed;
        [SerializeField] private float _distanceBeforeDrag;
        private Vector3 _defaultPosition;
        private Vector3 _targetPosition;
        private Vector2 _positionOfStartClick;
        private Coroutine _movingProcess;
        private Camera _camera;
        private bool _isDragging;
        private bool EndMoving
        {
            get
            {
                if (PlayerInputHandler.HoldClick) return false;
                if (!PlayerInputHandler.HoldClick && transform.position != _defaultPosition) return false;
                return true;
            }
        }

        private void Awake()
        {
            _defaultPosition = transform.position;
            _camera = Camera.main;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _positionOfStartClick = PlayerInputHandler.PointerPosition;
            _movingProcess = StartCoroutine(MoveProcess());
        }

        private void MoveToTarget()
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _movingSpeed);
        }
        private IEnumerator MoveProcess()
        {
            yield return null;
            GetTargetPosition();
            if (_distanceBeforeDrag < (_positionOfStartClick - PlayerInputHandler.PointerPosition).magnitude && !_isDragging) _isDragging = true;
            if (_isDragging) MoveToTarget();
            if (EndMoving)
            {
                _isDragging = false;
                StopCoroutine(_movingProcess);
            }
            else _movingProcess = StartCoroutine(MoveProcess());
        }
        private void GetTargetPosition()
        {
            if (PlayerInputHandler.HoldClick)
            {
                Ray ray = _camera.ScreenPointToRay(PlayerInputHandler.PointerPosition);
                RaycastHit[] hits = Physics.RaycastAll(ray);
                int count = hits.Length;
                for (int k = 0; k < count; k++)
                {
                    if (hits[k].collider.gameObject.TryGetComponent<Field>(out Field field))
                    {
                        _targetPosition = new Vector3(hits[k].point.x, transform.position.y, hits[k].point.z);
                        return;
                    }
                }
                _targetPosition = transform.position;
                return;
            }
            _targetPosition = _defaultPosition;
        }
    }
}