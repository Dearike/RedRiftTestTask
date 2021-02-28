using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.CardDealer;
using CodeBase.Infrastructure.Services.TextureProvider;
using CodeBase.Infrastructure.Services.TextureProvider.TextureLoader;
using CodeBase.Infrastructure.StateMachine.States;
using CodeBase.Logic;

namespace CodeBase.Infrastructure.StateMachine
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(
            TextureLoader textureLoader,
            SceneLoader sceneLoader,
            LoadingCurtain loadingCurtain,
            AllServices services)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
                [typeof(LoadTexturesState)] = new LoadTexturesState(this, textureLoader, services.Single<ITexturesProvider>()),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, loadingCurtain, services.Single<IGameFactory>(), services.Single<ICardDealerService>()),
                [typeof(DealingState)] = new DealingState(this, services.Single<ICardDealerService>()),
                [typeof(MatchLoopState)] = new MatchLoopState(this),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            ChangeState<TState>().Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            ChangeState<TState>().Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;

    }
}
