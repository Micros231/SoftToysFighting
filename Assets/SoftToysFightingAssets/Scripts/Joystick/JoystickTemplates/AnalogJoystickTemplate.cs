using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.SoftToysFighting.Joysticks
{
    public class AnalogJoystickTemplate : JoystickTemplate
    {
        public override JoystickTemplateType JoystickTemplateType => JoystickTemplateType.Axis;
        [SerializeField]
        private AnalogJoyButton
            _joystickButtonUP,
            _joystickButtonDOWN,
            _joystickButtonRIGHT,
            _joystickButtonLEFT;

        protected override void Update()
        {
            _verticalAxis = _joystickButtonUP.Float + _joystickButtonDOWN.Float;
            _horizontalAxis = _joystickButtonRIGHT.Float + _joystickButtonLEFT.Float;
        }

    }
}

