using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Com.SoftToysFighting.Joysticks
{
    public class StickJoystickTemplate : JoystickTemplate
    {
        public override JoystickTemplateType JoystickTemplateType => JoystickTemplateType.Axis;

        [SerializeField]
        private JoystickStick _joystickStick;

        protected override void Update()
        {
            _horizontalAxis = _joystickStick.Input.x;
            _verticalAxis = _joystickStick.Input.y;
        }
    }
}

