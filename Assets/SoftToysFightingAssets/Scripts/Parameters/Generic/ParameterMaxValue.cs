using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Com.SoftToysFighting
{
    public abstract class ParameterMaxValue<T> : ParameterValue<T> where T : struct 
    {
        public T MaxValue 
        {
            get => _maxValue;
            set 
            {
                _maxValue = value;
            } 
        }
        [SerializeField] private T _maxValue;
    }
}

