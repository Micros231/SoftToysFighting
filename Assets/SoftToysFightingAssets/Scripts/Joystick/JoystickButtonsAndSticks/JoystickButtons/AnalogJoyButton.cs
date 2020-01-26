using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Com.SoftToysFighting.Joysticks
{
    public class AnalogJoyButton : JoystickButton
    {
        public float Float;
        [SerializeField, Range(-1,1)]
        private int _direction;

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            if (_isSnap == true)
                Float = _direction;

        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            if(_isSnap == true)
                Float = 0;

        }
        private void Update()
        {
            if (_isSnap == false)
            {
                if (IsDown == true)
                {
                    if (_direction == 1)
                        Float = Float <= _direction ? Float + Time.deltaTime : _direction;
                    else if (_direction == -1)
                        Float = Float >= _direction ? Float - Time.deltaTime : _direction;
                }
                else
                {
                    if (_direction == 1)
                        Float = Float >= 0 ? Float - Time.deltaTime : 0;
                    else if (_direction == -1)
                        Float = Float <= 0 ? Float + Time.deltaTime : 0;
                }
            }
        }
    }
}

