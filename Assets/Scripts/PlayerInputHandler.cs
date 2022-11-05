using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameBoxClicker
{
    public class PlayerInputHandler : MonoBehaviour, PlayerInput.IGameActions
    {
        public static event Action OnclickEvent;
        public static Vector2 PointerPosition;
        public static bool HoldClick;

        private PlayerInput _playerInput;
        private bool _press;


        private void Awake()
        {
            _playerInput = new PlayerInput();
            _playerInput.Game.SetCallbacks(this);
            _playerInput.Game.Enable();
        }

        public void OnClick(InputAction.CallbackContext context)
        {
#if UNITY_EDITOR
            if (!_press)
            {
                HoldClick = true;
                OnclickEvent?.Invoke();
            }
            else HoldClick = false;
            _press = !_press;
#endif
#if PLATFORM_ANDROID
            if (context.phase==InputActionPhase.Started)
            {
                HoldClick = true;
                OnclickEvent?.Invoke();
            }
            if (context.phase == InputActionPhase.Performed) HoldClick = false;
#endif
        }
        public void OnPoint(InputAction.CallbackContext context)
        {
            PointerPosition = context.ReadValue<Vector2>();
        }

    }
}