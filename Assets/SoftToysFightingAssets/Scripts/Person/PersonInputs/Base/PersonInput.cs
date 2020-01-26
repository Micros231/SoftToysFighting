using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

namespace Com.SoftToysFighting.Person
{
    public class PersonInput : MonoBehaviour
    {
        #region Public Methods
        public virtual Vector2 GetAxisMove()
        {
            float horizontalAxis = Input.GetAxis("Horizontal");
            float verticalAxis = Input.GetAxis("Vertical");
            return new Vector2(horizontalAxis, verticalAxis);
        }
        public virtual bool Jump()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }
        public virtual bool AttackHand()
        {
            return Input.GetKeyDown(KeyCode.G);
        }
        public virtual bool AttackLeg()
        {
            return Input.GetKeyDown(KeyCode.H);
        }
        public virtual bool AttackSuper()
        {
            return Input.GetKeyDown(KeyCode.J);
        }
        #endregion
    }
}

