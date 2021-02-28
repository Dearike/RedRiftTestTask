using CodeBase.Logic.Stats;
using CodeBase.StaticData;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Logic
{
    [RequireComponent(typeof(Health), typeof(Attack), typeof(Mana))]
    public class Card : MonoBehaviour
    {
        [SerializeField] private Image _artImage;

        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private TextMeshProUGUI _healthText;
        [SerializeField] private TextMeshProUGUI _attackText;
        [SerializeField] private TextMeshProUGUI _manaText;

        [SerializeField] private Health _health;
        [SerializeField] private Attack _attack;
        [SerializeField] private Mana _mana;

        [SerializeField] private RectTransform _rectTransform;

        public Image Art => _artImage;

        public Health Health => _health;
        public Attack Attack => _attack;
        public Mana Mana => _mana;

        public Vector2 Pivot
        {
            get => _rectTransform.pivot;
            set => _rectTransform.pivot = value;
        }

        private void Awake()
        {
            transform.localScale = Vector3.one;
        }

        public void Initialize(CardStaticData cardStaticData, Texture2D texture)
        {
            _titleText.text = cardStaticData.Title;
            _descriptionText.text = cardStaticData.Description;
            
            _health.Changed += OnHealthChanged;
            _attack.Changed += OnAttackChanged;
            _mana.Changed += OnManaChanged;

            _health.Initialize(cardStaticData.Hp);
            _attack.Initialize(cardStaticData.Attack);
            _mana.Initialize(cardStaticData.Mana);

            SetArt(texture);
        }

        public void ResetPivot() =>
            Pivot = new Vector2(0.5f, 0.5f);

        private void SetArt(Texture2D texture)
        {
            _artImage.sprite = Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                Vector2.zero);
        }

        private void OnHealthChanged(int value)
        {
            _healthText.text = value.ToString();
            _healthText.transform.parent.DOPunchScale(Vector3.one, 0.5f, 5);
        }

        private void OnAttackChanged(int value)
        {
            _attackText.text = value.ToString();
            _attackText.transform.parent.DOPunchScale(Vector3.one, 0.5f, 5);
        }

        private void OnManaChanged(int value)
        {
            _manaText.text = value.ToString();
            _manaText.transform.parent.DOPunchScale(Vector3.one, 0.5f, 5);
        }

        private void OnDestroy()
        {
            _health.Changed -= OnHealthChanged;
            _attack.Changed -= OnAttackChanged;
            _mana.Changed -= OnManaChanged;
        }
    }
}