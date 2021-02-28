using System.Collections.Generic;
using CodeBase.Logic.Stats;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace CodeBase.Logic
{
    public enum Characteristics
    {
        Health,
        Attack,
        Mana
    }

    [RequireComponent(typeof(Button))]
    public class InteractiveTest : MonoBehaviour
    {
        [SerializeField] private Characteristics[] _testingStats;

        private Button _button;

        private List<Card> _cards;

        private int _index;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Interact);
        }

        private void Interact()
        {
            if (_cards == null)
                _cards = FindObjectOfType<CardHand>().Cards;

            if (_cards.Count == 0)
                return;

            if (_index >= _cards.Count)
                _index = 0;

            ChangeRandomCharacteristic(_cards[_index]);

            _index++;
        }

        private void ChangeRandomCharacteristic(Card card)
        {
            switch ((Characteristics)_testingStats.GetValue(Random.Range(0, _testingStats.Length)))
            {
                case Characteristics.Health:
                    card.GetComponent<Health>().Value = Random.Range(-2, 9);
                    break;

                case Characteristics.Attack:
                    card.GetComponent<Attack>().Value = Random.Range(-2, 9);
                    break;

                case Characteristics.Mana:
                    card.GetComponent<Mana>().Value = Random.Range(-2, 9);
                    break;
            }
        }
    }
}