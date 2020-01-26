using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Com.SoftToysFighting.Person
{
    [RequireComponent(typeof(PersonMovement), typeof(PersonAnimatorDragonBones),(typeof(PersonAttacker)))]
    public class PersonController : MonoBehaviour
    {
        #region Protected Fields
        protected PersonMovement PersonMovement { get; private set; }
        protected PersonAnimatorDragonBones PersonAnimator { get; private set; }
        protected PersonAttacker PersonAttacker { get; private set; }
        #endregion

        private PersonParameters PersonParameters { get; set; }
        private PersonInput PersonInput { get; set; }

        #region MonoBehaviour Callbacks
        private void Awake()
        {
            InitPersonSystems();
        }

        private void Update()
        {
            if (PersonAnimator.IsDead == false)
            {
                Attack();
            }
            
        }

        private void FixedUpdate()
        {
            if (PersonAnimator.IsDead == false)
            {
                Move();
            }
        }
        #endregion

        #region Protected Methods
        protected virtual void InitPersonSystems()
        {
            PersonMovement = GetComponent<PersonMovement>();          
            PersonAnimator = GetComponent<PersonAnimatorDragonBones>();
            PersonAttacker = GetComponent<PersonAttacker>();
            PersonParameters = GetComponent<PersonParameters>();
            PersonInput = GetComponent<PersonInput>();
            PersonMovement.SpeedMovement = PersonParameters.MoveSpeedParameter.Value;
        }
        protected virtual void Attack()
        {
            PersonAttacker.Attack(PersonInput.AttackHand(), PersonInput.AttackLeg(), PersonInput.AttackSuper());
        }
        protected virtual void Move()
        {
            PersonMovement.Move(PersonInput.GetAxisMove(), PersonInput.Jump());
        }
        #endregion
    }
}

