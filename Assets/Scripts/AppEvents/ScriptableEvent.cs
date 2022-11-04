using System;
using UnityEngine;

namespace GameBoxClicker.AppEvents
{
    [CreateAssetMenu(fileName = "new ScriptableEvent", menuName = "Scriptable Event/Scriptable Event")]
    public class ScriptableEvent : ScriptableObject
    {
        private event Action _listeners;

        public void Raise()
        {
            _listeners?.Invoke();
        }
        public void RegisterListener(Action listener)
        {
            _listeners += listener;
        }
        public void UnregisterListener(Action listener)
        {
            _listeners -= listener;
        }
    }
}