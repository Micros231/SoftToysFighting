using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

namespace Com.SoftToysFighting.Person
{
    public class PersonAnimatorDragonBones : MonoBehaviour
    {

        public int MoveDirection = 0;
        public float AnimationTimeScale;
        public bool IsJumping;
        public bool IsAttack;
        public bool IsDamageTake;
        public bool IsDead;

        private UnityArmatureComponent _armatureComponent;
        private DragonBones.AnimationState _walkState = null;
        private DragonBones.AnimationState _jumpState = null;
        private DragonBones.AnimationState _attackState = null;
        private DragonBones.AnimationState _damageTakeState = null;
        private DragonBones.AnimationState _deadState = null;

        #region MonoBehaviour Callbacks
        private void Start()
        {
            InitAnimator();
        }
        #endregion

        #region Public Methods

        public void MoveAnimation(int direction)
        {
            if (IsDead) return;
            if (MoveDirection != direction)
            {
                _walkState = null;
            }            
            else if (MoveDirection == direction)
            {
                return;
            }
            MoveDirection = direction;
            UpdateAnimation();
        }
        public void JumpAnimation(bool isJump)
        {
            if (IsDead) return;
            if (IsJumping)
            {
                if (_jumpState.isCompleted)
                {
                    IsJumping = false;
                    _jumpState = null;
                    SetIdleAnimation();
                    UpdateAnimation();
                }
                return;
            }
            if (!IsJumping && isJump)
            {
                IsJumping = true;
                _jumpState = _armatureComponent.animation.FadeIn("jump", -1.0f, -1, 0);
                _jumpState.resetToPose = false;
                _jumpState.playTimes = 1;
                _walkState = null;
            }
        }
        public void AttackAnimationGlobal(bool isAttack, ref bool isAttackType, string animationName)
        {
            if (IsDead) return;
            if (IsAttack && isAttackType)
            {
                if (_attackState.isCompleted)
                {
                    IsAttack = isAttackType = false;
                    _attackState = null;
                    SetIdleAnimation();
                    UpdateAnimation();
                }
                return;
            }
            if (!IsAttack && isAttack)
            {
                IsAttack = isAttackType = true;
                _attackState = _armatureComponent.animation.FadeIn(animationName, -1.0f, -1, 0);
                _attackState.resetToPose = false;
                _attackState.playTimes = 1;
                _walkState = null;
            }
        }
        public void DamageTakeAnimation()
        {
            if (!IsAttack)
            {
                IsDamageTake = true;
                _damageTakeState = null;
                _damageTakeState = _armatureComponent.animation.FadeIn("get_damage", -1.0f, -1, 0);
                _damageTakeState.resetToPose = false;
                _damageTakeState.playTimes = 1;
                _damageTakeState.timeScale = 2;
                _walkState = null;
                StartCoroutine(TakeDamageAnimationCoroutine());
            }
            

        }
        public void DeadAnimation()
        {
            IsDead = true;
            _deadState = _armatureComponent.animation.FadeIn("dead", -1.0f, -1, 0);
            _deadState.resetToPose = false;
            _deadState.playTimes = 1;
            _walkState = null;
        }
        public void SetFlipX(bool flip)
        {
            _armatureComponent.armature.flipX = flip;
        }
       
        #endregion


        #region Private Methods
        private void UpdateAnimation()
        {
            if (IsDead == false)
            { 
                if (IsJumping)
                {
                    return;
                }
                if (IsAttack)
                {
                    return;
                }
                if (MoveDirection == 0)
                {
                    SetIdleAnimation();
                }
                else
                {
                    if (_walkState == null)
                    {
                        if (MoveDirection == -1 || MoveDirection == 1)
                        {
                            _walkState = _armatureComponent.animation.FadeIn("walk", -1.0f, -1, 0);
                            this._walkState.resetToPose = false;
                        }
                        else if (MoveDirection == 2)
                        {
                            _walkState = _armatureComponent.animation.FadeIn("walk_up", -1.0f, -1, 0);
                            this._walkState.resetToPose = false;
                        }

                    }

                }
            }
            
        }
        private void SetIdleAnimation()
        {
            _armatureComponent.animation.FadeIn("idle", -1.0f, -1, 0).resetToPose = false;
            _walkState = null;
        }
        private IEnumerator TakeDamageAnimationCoroutine()
        {
            yield return new WaitWhile(() => _damageTakeState.isCompleted == false);
            IsDamageTake = false;
            _damageTakeState = null;
            SetIdleAnimation();
            UpdateAnimation();

        }
        private void SetNullAnimation()
        {
            IsDamageTake = false;
            IsAttack = false;
            IsJumping = false;
            _attackState = null;
            _damageTakeState = null;
            _jumpState = null;
            _walkState = null;
        }
        private void InitAnimator()
        {
            _armatureComponent = GetComponent<UnityArmatureComponent>();
            _armatureComponent.animation.timeScale = AnimationTimeScale;
            _armatureComponent.animation.Reset();
            _armatureComponent.animation.Play("idle");
        }
        

        #endregion
    }
}

