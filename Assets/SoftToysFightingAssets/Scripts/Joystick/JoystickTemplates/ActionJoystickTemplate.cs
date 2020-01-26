using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Com.SoftToysFighting.Joysticks
{
    public class ActionJoystickTemplate : JoystickTemplate
    {
        public override JoystickTemplateType JoystickTemplateType => JoystickTemplateType.Action;
        [SerializeField]
        private List<ActionJoyButton> _actionJoyButtons = new List<ActionJoyButton>();
        private void Awake()
        {
            foreach (var actionButton in _actionJoyButtons)
            {
                _actionBools.Add(new ActionBool { Name = actionButton.Name });
            }
        }
        protected override void Update()
        {
            for (int i = 0; i < _actionBools.Count; i++)
            {
                _actionBools[i].IsDown = _actionJoyButtons[i].IsDown;
            }
        }
    }
}

