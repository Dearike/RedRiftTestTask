using System;
using CodeBase.Logic;

namespace CodeBase.Infrastructure.Services.CardDealer
{
    public interface ICardDealerService : IService
    {
        void Deal(Action onDealed);
        void AddHand(CardHand hand);
    }
}