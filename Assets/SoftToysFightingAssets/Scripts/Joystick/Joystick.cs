using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Com.SoftToysFighting.Joysticks
{
    public class Joystick : MonoBehaviour
    {
        #region Public Properties
        public float Horizontal { get => input.x; }
        public float Vertical { get => input.y; }
        public Vector2 Direction { get => new Vector2(Horizontal, Vertical); }

        public List<ActionBool> ActionBools => ActionBridge.CurrentJoystick.ActionBools;

        public JoystickSettings JoystickSettings { get; set; }
        #endregion

        #region Public Fields
        public List<JoystickTemplateBridge> JoystickTemplateBridges = new List<JoystickTemplateBridge>();
        #endregion

        #region Private Fields
        private Vector2 input;
        [SerializeField]
        private JoystickTemplateBridge AxisBridge, ActionBridge;
        #endregion

        #region Monobehaviour Callbacks
        private void Awake()
        {
            InitJoy();
        }

        private void Update()
        {
            input = AxisBridge.CurrentJoystick.InputVector;
        }
        #endregion

        #region Private Methods
        private void InitJoy()
        {
            JoystickTemplateBridges = GetComponentsInChildren<JoystickTemplateBridge>().ToList();
            AxisBridge = JoystickTemplateBridges.FirstOrDefault(bridge => bridge.JoystickTemplateBridgeType == JoystickTemplateBridgeType.Axis);
            ActionBridge = JoystickTemplateBridges.FirstOrDefault(bridge => bridge.JoystickTemplateBridgeType == JoystickTemplateBridgeType.Action);
        }
        #endregion
    }
}

