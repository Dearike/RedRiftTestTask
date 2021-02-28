using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.CardDealer;
using CodeBase.Logic;

namespace CodeBase.Infrastructure.StateMachine.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly ICardDealerService _dealer;
        
        public LoadLevelState(
            GameStateMachine stateMachine,
            SceneLoader sceneLoader,
            LoadingCurtain curtain,
            IGameFactory gameFactory,
            ICardDealerService dealer)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _dealer = dealer;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _curtain.Hide();
        }

        private void OnLoaded()
        {
            InitGameWorld();
            _stateMachine.Enter<DealingState>();
        }

        private void InitGameWorld()
        {
            InitUIRoot();
            InitGameBoard();
        }

        private void InitUIRoot()
        {
            _gameFactory.CreateUIRoot();
        }

        private void InitGameBoard()
        {
            GameBoard board = _gameFactory.CreateGameBoard();

            foreach (CardHand hand in board.Hands)
                _dealer.AddHand(hand);
        }
    }   
}