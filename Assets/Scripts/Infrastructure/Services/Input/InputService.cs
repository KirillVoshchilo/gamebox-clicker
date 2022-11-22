using UnityEngine;
using UnityEngine.InputSystem;

namespace GameBoxClicker.Services.Input
{
    public class InputService : PlayerInput.IGameActions, IInputService
    {

        private PlayerInput _playerInput;

        public bool HoldClick { get; set; }
        public Vector2 PointerPosition { get; set; }

        public InputService()
        {
            HoldClick = false;
            _playerInput = new PlayerInput();
            _playerInput.Game.SetCallbacks(this);
            _playerInput.Game.Enable();
        }
        public void OnClick(InputAction.CallbackContext context)
        {
            HoldClick = context.control.IsPressed();
        }
        public void OnPoint(InputAction.CallbackContext context)
        {
            PointerPosition = context.ReadValue<Vector2>();
        }
    }
}