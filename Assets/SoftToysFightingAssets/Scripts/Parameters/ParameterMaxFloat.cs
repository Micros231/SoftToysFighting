using System;
using UnityEngine;
using UnityEngine.Events;

namespace Com.SoftToysFighting
{
    [Serializable]
    public class ParameterMaxFloat : ParameterMaxValue<float>
    {
        public override UnityEvent<float> OnChangeValue
        {
            get => _onChangeValue;
            set => _onChangeValue = (ChangeFloatValueEvent)value;
        }
        [SerializeField]
        private ChangeFloatValueEvent _onChangeValue;
    }
}

