using GameBoxClicker.AppEvents;
using UnityEngine;

namespace GameBoxClicker
{
    public class AppManager : MonoBehaviour
    {
        [SerializeField] private ScriptableEvent _onAppExit;

        private void Awake()
        {
            _onAppExit.RegisterListener(ExitApp);
        }
        private void OnDestroy()
        {
            _onAppExit.UnregisterListener(ExitApp);
        }
        private void ExitApp()
        {
            Application.Quit();
        }
    }
}