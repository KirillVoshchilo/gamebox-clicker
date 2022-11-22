using GameBoxClicker.AppEvents;
using UnityEngine;

namespace GameBoxClicker
{
    [CreateAssetMenu(fileName = "new ContentSettings", menuName = "ContentSettings")]
    public class ContentSettings : ScriptableObject, IStartNewGame
    {
        [SerializeField] private float _defaultEarnPerClick;
        [SerializeField] private float _defaultIncome;
        [SerializeField] private float _defaultDelayBetweenIncome;
        [SerializeField] private ScriptableEvent _onStartNewGame;

        public float EarnPerClick { get; set; }
        public float Income { get; set; }
        public float DelayBetweenIncome { get; set; }
        private float EarnPerClickMultiplier { get; set; }
        private float IncomeMultiplier { get; set; }
        public int UpgradesLevel { get; set; }

        private void OnEnable()
        {
            _onStartNewGame.RegisterListener(AssignDefaultValues);
        }
        private void OnDisable()
        {
            _onStartNewGame.UnregisterListener(AssignDefaultValues);
        }

        public void SetEarnPerClickMultiplier(float value)
        {
            EarnPerClickMultiplier = value;
            EarnPerClick = _defaultEarnPerClick * EarnPerClickMultiplier;
        }
        public void SetIncomeMultiplier(float value)
        {
            IncomeMultiplier = value;
            Income = _defaultIncome * IncomeMultiplier;
        }
        public void StartNewGame()
        {
            AssignDefaultValues();
        }

        private void AssignDefaultValues()
        {
            EarnPerClick = _defaultEarnPerClick;
            Income = _defaultIncome;
            DelayBetweenIncome = _defaultDelayBetweenIncome;
            UpgradesLevel = 1;
            EarnPerClickMultiplier = 1;
            IncomeMultiplier = 1;
        }
    }
}