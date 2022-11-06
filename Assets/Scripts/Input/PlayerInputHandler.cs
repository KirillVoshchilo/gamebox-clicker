using UnityEngine;
using UnityEngine.InputSystem;

namespace GameBoxClicker.Input
{
    public class PlayerInputHandler : MonoBehaviour, PlayerInput.IGameActions
    {
        public static Vector2 PointerPosition;
        public static bool HoldClick;

        private PlayerInput _playerInput;

        private void Awake()
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