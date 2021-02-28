using UnityEngine;
using CodeBase.Infrastructure.Services.AssetProvider;
using CodeBase.Infrastructure.Services.TextureProvider;
using CodeBase.Logic;
using CodeBase.StaticData;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly ITexturesProvider _texturesProvider;
        private readonly IStaticDataService _staticDataService;

        private Transform _uiRoot;

        public GameFactory(
            IAssetProvider assetProvider,
            ITexturesProvider texturesProvider,
            IStaticDataService staticDataService)
        {
            _assetProvider = assetProvider;
            _texturesProvider = texturesProvider;
            _staticDataService = staticDataService;
        }

        public Transform CreateUIRoot()
        {
            _uiRoot = _assetProvider.Instantiate(AssetPath.UIRootPath).transform;
            return _uiRoot;
        }

        public GameBoard CreateGameBoard()
        {
            GameBoard gameBoard = _assetProvider
                .Instantiate(AssetPath.GameBoardPath, _uiRoot)
                .GetComponent<GameBoard>();

            return gameBoard;
        }

        public Card CreateCard(CardTypeId cardTypeId, Transform parent)
        {
            CardStaticData cardStaticData = _staticDataService.ForCard(cardTypeId);

            Texture2D texture = _texturesProvider.GetTextureByTypeId(cardTypeId);

            Card card = _assetProvider
                .Instantiate(AssetPath.CardPath, parent)
                .GetComponent<Card>();

            card.Initialize(cardStaticData, texture);

            return card;
        }
    }
}