using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.AssetProvider;
using CodeBase.Infrastructure.Services.CardDealer;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.Random;
using CodeBase.Infrastructure.Services.TextureProvider;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.StateMachine.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(
            GameStateMachine stateMachine,
            SceneLoader sceneLoader,
            AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(SceneNames.Initial, onLoaded: EnterLoadLevel);
        }

        public void Exit()
        {

        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadTexturesState>();
        }

        private void RegisterServices()
        {
            RegisterStaticData();

            _services.RegisterSingle<IRandomService>(new UnityRandomService());
            _services.RegisterSingle<ITexturesProvider>(new PicsumTexturesProvider());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IInputServise>(GetInputService());

            _services.RegisterSingle<IGameFactory>(new GameFactory(
                _services.Single<IAssetProvider>(),
                _services.Single<ITexturesProvider>(),
                _services.Single<IStaticDataService>()));
            
            _services.RegisterSingle<ICardDealerService>(new RandomCardDealer(
                _services.Single<IGameFactory>(),
                _services.Single<IRandomService>()));
        }

        private void RegisterStaticData()
        {
            IStaticDataService staticData = new StaticDataService();
            staticData.Load();
            _services.RegisterSingle<IStaticDataService>(staticData);
        }

        private static IInputServise GetInputService()
        {
            if (Application.isEditor)
                return new StandaloneInputService();
            else
                return new MobileInputService();
        }
    }
}