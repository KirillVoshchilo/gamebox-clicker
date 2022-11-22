﻿using GameBoxClicker.Infrastructure.Factory;
using GameBoxClicker.Infrastructure.Services;
using GameBoxClicker.Infrastructure.Services.PersistentProgress;
using GameBoxClicker.Infrastructure.Services.SaveLoad;
using System;
using System.Collections.Generic;

namespace GameBoxClicker.Infrastructure.States
{
    public class GameStateMachine
    {
        private Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, AllServices services)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
                [typeof(LoadLevelState)] = new LoadLevelState(this,sceneLoader,loadingCurtain,services.Single<IGameFactory>(),services.Single<IPersistentProgressService>()),
           //     [typeof(LoadProgressState)]= new LoadProgressState(this, services.Single<IPersistentProgressService>(),services.Single<ISaveLoadService>()),
                [typeof(GameLoopState)]=new GameLoopState(this),
            };
        }
        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }
        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }
        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}