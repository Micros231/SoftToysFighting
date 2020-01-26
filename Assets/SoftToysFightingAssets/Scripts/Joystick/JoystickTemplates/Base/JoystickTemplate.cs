using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Com.SoftToysFighting.Joysticks
{
    public enum JoystickTemplateType { Axis, Action }
    public class JoystickTemplate : MonoBehaviour
    {
        #region Public Properties
        public Vector2 InputVector { get => new Vector2(_horizontalAxis, _verticalAxis); }
        public List<ActionBool> ActionBools { get => _actionBools; }
        public virtual JoystickTemplateType JoystickTemplateType { get; }
        #endregion

        #region Protected Fields
        protected float _horizontalAxis;
        protected float _verticalAxis;
        #endregion

        #region Protected SerializeFields
        [SerializeField]
        protected List<ActionBool> _actionBools = new List<ActionBool>();
        #endregion


        protected virtual void Update()
        {
            
        }
    }
}

