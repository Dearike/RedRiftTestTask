using CodeBase.Infrastructure.Services.CardDealer;

namespace CodeBase.Infrastructure.StateMachine.States
{
    public class DealingState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly ICardDealerService _dealer;

        public DealingState(
            GameStateMachine stateMachine,
            ICardDealerService dealer)
        {
            _stateMachine = stateMachine;
            _dealer = dealer;
        }

        public void Enter()
        {
            _dealer.Deal(OnDealed);
        }

        public void Exit()
        {

        }

        private void InitCards()
        {

        }

        private void OnDealed()
        {
            _stateMachine.Enter<MatchLoopState>();
        }
    }
}