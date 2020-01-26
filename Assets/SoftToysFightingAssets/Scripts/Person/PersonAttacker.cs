using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Com.SoftToysFighting.Person
{
    public class PersonAttacker : MonoBehaviour
    {
        #region Private SerializeFields
        [SerializeField]
        private PersonDamager 
            _personDamagerHand, 
            _personDamagerLeg, 
            _personDamagerSuper;
        #endregion
        #region Protected Fields
        protected PersonAnimatorDragonBones _personAnimator;
        protected PersonParameters _personParameters;
        #endregion


        #region Private Fields
        private Collider2D
            _personDamagerHandCollider2D,
            _personDamagerLegCollider2D,
            _personDamagerSuperCollider2D;
        private bool _isAttackHand, _isAttackLeg, _isAttackSuper;
        #endregion


        private void Awake()
        {
            InitPersonAttacker();
        }

        public void Attack(bool attackHand, bool attackLeg, bool attackSuper)
        {
            if (!_personAnimator.IsJumping && !_personAnimator.IsDead && !_personAnimator.IsDamageTake)
            {
                AttackHand(attackHand);
                AttackLeg(attackLeg);
                AttackSuper(attackSuper);
            }
            
        }

        protected virtual void AttackHand(bool isAttack)
        {
            _personAnimator.AttackAnimationGlobal(isAttack, ref _isAttackHand , "attack_hand");
            _personDamagerHandCollider2D.enabled = _isAttackHand;
        }
        protected virtual void AttackLeg(bool isAttack)
        {
            _personAnimator.AttackAnimationGlobal(isAttack, ref _isAttackLeg , "attack_leg");
            _personDamagerLegCollider2D.enabled = _isAttackLeg;
        }
        protected virtual void AttackSuper(bool isAttack)
        {
            _personAnimator.AttackAnimationGlobal(isAttack, ref _isAttackSuper, "attack_super");
            _personDamagerSuperCollider2D.enabled = _isAttackSuper;
        }

        protected virtual void InitPersonAttacker()
        {
            _personAnimator = GetComponent<PersonAnimatorDragonBones>();
            _personParameters = GetComponent<PersonParameters>();
            _personDamagerHandCollider2D = _personDamagerHand.InitPersonDamager(_personParameters.DamageHandParameter.Value, this.transform, _personAnimator);
            _personDamagerLegCollider2D = _personDamagerLeg.InitPersonDamager(_personParameters.DamageLegParameter.Value, this.transform, _personAnimator);
            _personDamagerSuperCollider2D = _personDamagerSuper.InitPersonDamager(_personParameters.DamageSuperParameter.Value, this.transform, _personAnimator);
        }
    }
}

