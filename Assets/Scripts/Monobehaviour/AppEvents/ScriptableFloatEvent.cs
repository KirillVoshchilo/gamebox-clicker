using System;
using UnityEngine;

namespace GameBoxClicker.AppEvents
{
    [CreateAssetMenu(fileName = "new ScriptableFloatEvent", menuName = "Scriptable Event/Scriptable Float Event")]
    public class ScriptableFloatEvent : ScriptableObject
    {
        private event Action<float> Listeners;

        public void Raise(float value)
        {
            Listeners?.Invoke(value);
        }
        public void RegisterListener(Action<float> listener)
        {
            Listeners += listener;
        }
        public void UnregisterListener(Action<float> listener)
        {
            Listeners -= listener;
        }
    }
}