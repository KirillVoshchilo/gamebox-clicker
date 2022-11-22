using System;
using UnityEngine;

namespace GameBoxClicker.AppEvents
{
    [CreateAssetMenu(fileName = "new ScriptableContentSettingsEvent", menuName = "Scriptable Event/Scriptable Content Settings Event")]
    public class ScriptableContentSettingsEvent : ScriptableObject
    {
        private event Action<ContentSettings> Listeners;

        public void Raise(ContentSettings contentSettings)
        {
            Listeners?.Invoke(contentSettings);
        }
        public void RegisterListener(Action<ContentSettings> listener)
        {
            Listeners += listener;
        }
        public void UnregisterListener(Action<ContentSettings> listener)
        {
            Listeners -= listener;
        }
    }
}