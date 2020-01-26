using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Com.SoftToysFighting.Joysticks
{
    public class JoystickButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public bool IsDown { get; protected set; }
        [SerializeField]
        protected bool _isSnap;

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            IsDown = true;
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            IsDown = false;
        }

    }
}

