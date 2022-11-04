using System;
using UnityEngine;

namespace GameBoxClicker.AppEvents
{
    [CreateAssetMenu(fileName = "new ScriptableIntEvent", menuName = "Scriptable Event/Scriptable Int Event")]
    public class ScriptableIntEvent : ScriptableObject
    {
        private event Action<int> _listeners;

        public void Raise(int value)
        {
            _listeners?.Invoke(value);
        }
        public void RegisterListener(Action<int> listener)
        {
            _listeners += listener;
        }
        public void UnregisterListener(Action<int> listener)
        {
            _listeners -= listener;
        }
    }
}
