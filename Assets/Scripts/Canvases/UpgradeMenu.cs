using UnityEngine;

namespace GameBoxClicker.Canvases
{
    public class UpgradeMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _upgradesMenu;
        private bool _isOpen;

        public void ClickOnUpgradeMenuButton()
        {
            if (_isOpen) _upgradesMenu.SetActive(false);
            else _upgradesMenu.SetActive(true);
            _isOpen = !_isOpen;
        }
    }
}