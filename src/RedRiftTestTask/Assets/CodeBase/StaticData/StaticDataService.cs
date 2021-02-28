using System.Collections.Generic;
using System.Linq;
using CodeBase.Infrastructure;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string StaticDataCardsPath = "StaticData/Cards";

        private Dictionary<CardTypeId, CardStaticData> _cards;

        public void Load()
        {
            _cards = Resources
                .LoadAll<CardStaticData>(StaticDataCardsPath)
                .ToDictionary(x => x.CardTypeId, x => x);
        }

        public CardStaticData ForCard(CardTypeId typeId)
        {
            return _cards.TryGetValue(typeId, out CardStaticData staticData) 
                ? staticData
                : null;
        }
    }
}