using GameBoxClicker.Infrastructure.Data;
using GameBoxClicker.Infrastructure.Services.PersistentProgress;
using GameBoxClicker.Infrastructure.Services.SaveLoad;
using System;

namespace GameBoxClicker.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressSrevice;
        private readonly ISaveLoadService _saveLoadProgress;

        public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoadProgress)
        {
            _gameStateMachine = gameStateMachine;
            _progressSrevice = progressService;
            _saveLoadProgress = saveLoadProgress;
        }
        public void Enter()
        {
            LoadProgressOrInitNew();
            _gameStateMachine.Enter<LoadLevelState, string>("something");
        }

        private void LoadProgressOrInitNew()
        {
            _progressSrevice.Progress = _saveLoadProgress.LoadProgress()
                ?? NewProgress();
        }

        private PlayerProgress NewProgress()=>
            new PlayerProgress(initialLevel: "Main");

        public void Exit()
        {
        }
    }
}