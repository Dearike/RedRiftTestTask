using System;
using UnityEngine;

namespace CodeBase.Logic.Stats
{
    public abstract class Characteristic : MonoBehaviour
    {
        protected int _startValue;
        protected int _currentValue;
        
        public event Action<int> Changed;

        public int StartValue
        {
            get => _startValue;
            private set => _startValue = value;
        }

        public int Value
        {
            get => _currentValue;
            set
            {
                if (_currentValue != value)
                {
                    _currentValue = value;
                    Changed?.Invoke(_currentValue);
                }
            }
        }

        public virtual void Initialize(int value)
        {
            StartValue = value;
            Value = value;
            Changed += OnChanged;
        }

        public virtual void ResetValue()
        {
            _currentValue = _startValue;
        }

        protected virtual void OnChanged(int value)
        {
            //shake
        }

        protected virtual void OnDestroy()
        {
            Changed -= OnChanged;
        }
    }
}