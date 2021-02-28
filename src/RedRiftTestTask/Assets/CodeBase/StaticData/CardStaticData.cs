using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "CardData", menuName = "StaticData/Card")]
    public class CardStaticData : ScriptableObject
    {
        public string Title;

        [TextArea(3, 5)]
        public string Description;

        public CardTypeId CardTypeId;

        [Range(0, 99)]
        public int Hp;

        [Range(0f, 99)]
        public int Attack;

        [Range(0f, 99)]
        public int Mana;
    }
}