using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.TextureProvider.TextureLoader;
using CodeBase.Infrastructure.StateMachine;
using CodeBase.Logic;

namespace CodeBase.Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain)
        {
            StateMachine = new GameStateMachine(
                new TextureLoader(coroutineRunner),
                new SceneLoader(coroutineRunner),
                loadingCurtain,
                AllServices.Container);
        }
    }
}
