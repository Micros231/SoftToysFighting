using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Com.SoftToysFighting
{
    public abstract class ParameterValue<T> : Parameter where T : struct
    {
        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                OnChangeValue.Invoke(_value);
            }
        }
        public abstract UnityEvent<T> OnChangeValue { get; set; }
        [SerializeField] private T _value;
    }

    [Serializable]
    public class ChangeFloatValueEvent : UnityEvent<float> { }
}

