using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Logic
{
    public class CardHand : MonoBehaviour
    {
        [SerializeField] private float _twistAngle;
        [SerializeField] private float _maxAngle;
        [SerializeField] private RectTransform _pivot;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private float _repositionDuration = 1f;

        private List<Card> _cards = new List<Card>();

        //TODO: For Interactive Test
        public List<Card> Cards => _cards;

        private float _radius;

        private Vector3 Pivot
        {
            get => _pivot.position;
            set => _pivot.position = value;
        }

        private void Awake()
        {
            _radius =
                Mathf.Abs(_pivot.position.y) +
                _rectTransform.sizeDelta.y / 2f + 30;
        }

        public void Add(Card card)
        {
            _cards.Add(card);

            card.GetComponent<Death>().Died += Remove;

            UpdateCardPositions();
        }

        public void Remove(Card card)
        {
            card.GetComponent<Death>().Died -= Remove;

            _cards.Remove(card);

            UpdateCardPositions();
        }

        private void UpdateCardPositions()
        {
            for (int i = 0; i < _cards.Count; i++)
            {
                float angleFactor = 0f;

                if (_cards.Count % 2 == 0)
                    angleFactor = _cards.Count / (-2) + i + 0.5f;
                else
                    angleFactor = _cards.Count / (-2) + i;

                PositionCard(_cards[i], angleFactor);
            }
        }

        private void PositionCard(Card card, float angleFactor)
        {
            float eulers = _maxAngle * angleFactor;
            float radians = eulers * Mathf.Deg2Rad;

            Vector3 position = Pivot + new Vector3(
                _radius * Mathf.Sin(radians),
                _radius * Mathf.Cos(radians),
                0f);
            
            card.transform.DOMove(position, _repositionDuration);
            card.transform.DORotate(new Vector3(0f, 0f, -eulers - _twistAngle), _repositionDuration);
        }
    }
}