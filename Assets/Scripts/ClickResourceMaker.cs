using GameBoxClicker.AppEvents;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameBoxClicker
{
    public class ClickResourceMaker : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private int _earnPerTime;

        [SerializeField] private ScriptableIntEvent _onEarnResources;

        public void OnPointerClick(PointerEventData eventData)
        {
            _onEarnResources.Raise(_earnPerTime);
        }
    }
}