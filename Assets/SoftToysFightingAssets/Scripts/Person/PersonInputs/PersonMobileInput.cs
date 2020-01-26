using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Com.SoftToysFighting.Joysticks;

namespace Com.SoftToysFighting.Person
{
    public class PersonMobileInput : PersonInput
    {
        [SerializeField]
        private Joysticks.Joystick _joystick;
        [SerializeField]
        private GameObject _joystickPrefab;
        [SerializeField]
        private Canvas _canvas;

        private void Awake()
        {
            if (GameObject.FindWithTag("Joystick") != null)
            {
                _joystick = GameObject.FindWithTag("Joystick").GetComponent<Joysticks.Joystick>();
                Debug.Log($"{gameObject.name} find Joystick");
            }
            else
            {
                GameObject joystick = Instantiate(_joystickPrefab,_canvas.transform);
                _joystick = joystick.GetComponent<Joysticks.Joystick>();
            }
            
        }

        #region Public Methods
        public override Vector2 GetAxisMove()
        {          
            return new Vector2(_joystick.Horizontal, _joystick.Vertical);
        }
        public override bool Jump()
        {
            return ActionBoolIsDown("Jump");
        }
        public override bool AttackHand()
        {
            return ActionBoolIsDown("Hand");
        }
        public override bool AttackLeg()
        {
            return ActionBoolIsDown("Leg");
        }
        public override bool AttackSuper()
        {
            return ActionBoolIsDown("Super");
        }
        #endregion
        private bool ActionBoolIsDown(string name)
        {
            ActionBool actionBool = _joystick.ActionBools.FirstOrDefault(action => action.Name == name);
            return actionBool.IsDown;
        }
    }
}

