using GameBoxClicker.Infrastructure.Services;
using GameBoxClicker.Infrastructure.States;

namespace GameBoxClicker.Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine;
        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner),curtain,AllServices.Container);
        }
    }
}