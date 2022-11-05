using System;
using UnityEngine;

namespace GameBoxClicker
{
    public class PlayerInputHandler : MonoBehaviour, PlayerInput.IGameActions
    {

        public static event Action OnclickEvent;
        private PlayerInput _playerInput;

        private bool _press;

        private void Awake()
        {
            _playerInput = new PlayerInput();
            _playerInput.Game.SetCallbacks(this);
            _playerInput.Game.Enable();
        }

        public void OnClick(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
#if UNITY_EDITOR
            if (!_press) OnclickEvent?.Invoke();
            _press = !_press;
#endif
#if PLATFORM_ANDROID
            OnclickEvent?.Invoke();
#endif
        }

        public void OnPoint(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {

        }
    }
}