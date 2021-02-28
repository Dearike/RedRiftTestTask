using CodeBase.Infrastructure.Services;
using CodeBase.Logic;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        Transform CreateUIRoot();
        GameBoard CreateGameBoard();
        Card CreateCard(CardTypeId cardTypeId, Transform parent);
    }
}