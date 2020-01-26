using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Com.SoftToysFighting.Joysticks
{
    public enum JoystickTemplateBridgeType { Axis, Action}
    public class JoystickTemplateBridge : MonoBehaviour
    {
        public List<JoystickTemplate> JoystickTemplates = new List<JoystickTemplate>();
        public JoystickTemplateBridgeType JoystickTemplateBridgeType;
        public JoystickTemplate CurrentJoystick;

        private void Awake()
        {
            JoystickTemplates = GetComponentsInChildren<JoystickTemplate>().ToList();
        }
    }
}

