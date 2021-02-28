using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.Random;
using CodeBase.Logic;
using CodeBase.StaticData;

namespace CodeBase.Infrastructure.Services.CardDealer
{
    public class RandomCardDealer : ICardDealerService
    {
        private List<CardHand> _hands = new List<CardHand>();
        private IGameFactory _gameFactory;
        private IRandomService _random;

        public RandomCardDealer(IGameFactory gameFactory, IRandomService random)
        {
            _gameFactory = gameFactory;
            _random = random;
        }

        public void AddHand(CardHand hand)
        {
            _hands.Add(hand);
        }

        public void Deal(Action onDealed = null)
        {
            foreach (CardHand hand in _hands)
                FillHand(hand);

            onDealed?.Invoke();
        }

        private void FillHand(CardHand hand)
        {
            for (int i = 0; i < _random.Next(4, 6); i++)
            {
                Card card = _gameFactory.CreateCard(
                    GetRandomCardTypeId(),
                    hand.transform);

                hand.Add(card);
            }
        }

        private CardTypeId GetRandomCardTypeId()
        {
            var types = Enum.GetValues(typeof(CardTypeId));
            return (CardTypeId)types.GetValue(_random.Next(0, types.Length));
        }
    }
}