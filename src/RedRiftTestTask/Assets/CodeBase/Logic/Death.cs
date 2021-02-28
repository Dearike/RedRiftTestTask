using System;
using CodeBase.Logic.Stats;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Logic
{
    [RequireComponent(typeof(Card), typeof(Health))]
    public class Death : MonoBehaviour
    {
        [SerializeField] private Card _card;
        [SerializeField] private Health _health;
        [SerializeField] private Attack _attack;
        [SerializeField] private float _deathEffectDuration = 1f;

        public event Action<Card> Died;

        private bool _isDead;

        private void Start()
        {
            _health.Changed += OnHealthChanged;
        }

        private void OnDestroy()
        {
            _health.Changed -= OnHealthChanged;
        }

        private void OnHealthChanged(int value)
        {
            if (!_isDead && value <= 0)
                Die();
        }

        private void Die()
        {
            _isDead = true;
            
            if(_attack != null)
                _attack.enabled = false;

            Died?.Invoke(_card);

            PlayDeathEffect();
        }

        private void PlayDeathEffect()
        {
            transform
                .DOScale(0, _deathEffectDuration)
                .SetEase(Ease.InBack)
                .OnComplete(() => Destroy(gameObject));
        }
    }
}