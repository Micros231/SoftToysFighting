using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Com.SoftToysFighting.Joysticks
{
    [System.Serializable]
    public class ActionJoyButton : JoystickButton
    {
        public string Name;
        [SerializeField]
        private bool _isClick;

        public override void OnPointerDown(PointerEventData eventData)
        {
            if (_isSnap == false)
            {
                base.OnPointerDown(eventData);
            }
            else
            {
                _isClick = true;
            }
        }
        public override void OnPointerUp(PointerEventData eventData)
        {
            if (_isSnap == false)
            {
                base.OnPointerUp(eventData);
            }
            
        }
        private void Update()
        {
            if (_isClick == true && _isSnap == true)
            {
                IsDown = true;
                _isClick = false;
            }
            else if (_isClick == false && _isSnap == true)
            {
                IsDown = false;
            }
        }
    }
}

