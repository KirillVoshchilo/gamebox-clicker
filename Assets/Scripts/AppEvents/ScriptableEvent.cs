using System;
using UnityEngine;

namespace GameBoxClicker.AppEvents
{
    [CreateAssetMenu(fileName = "new ScriptableEvent", menuName = "Scriptable Event/Scriptable Event")]
    public class ScriptableEvent : ScriptableObject
    {
        private event Action Listeners;

        public void Raise()
        {
            Listeners?.Invoke();
        }
        public void RegisterListener(Action listener)
        {
            Listeners += listener;
        }
        public void UnregisterListener(Action listener)
        {
            Listeners -= listener;
        }
    }
}