using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Doozy.Engine.Progress;

namespace Com.SoftToysFighting.Person
{
    public class PersonParameters : MonoBehaviour
    {
        public ParameterMaxFloat HealthParameter = new ParameterMaxFloat();
        public Progressor HpProgressor;
        public ParameterFloat DamageHandParameter = new ParameterFloat();
        public ParameterFloat DamageLegParameter = new ParameterFloat();
        public ParameterFloat DamageSuperParameter = new ParameterFloat();
        public ParameterFloat MoveSpeedParameter = new ParameterFloat();
        public ParameterFloat AnimationTimeScaleParameter = new ParameterFloat();

        public HitEvent HitEvent;
        public event EventHandler DeadEvent;
        
        private PersonAnimatorDragonBones _personAnimator;

        private void Awake()
        {
            InitPersonParameters();
        }

        public void TakeDamage(float damage)
        {
            if (_personAnimator.IsDead == false && _personAnimator.IsJumping == false)
            {
                HealthParameter.Value -= damage;
                HitEvent.Invoke(damage);
                if (HealthParameter.Value <= 0)
                {
                    _personAnimator.DeadAnimation();
                    Destroy(GetComponent<Collider2D>());
                    if (DeadEvent != null)
                    {
                        DeadEvent.Invoke(this, new EventArgs());
                    }
                  
                    Destroy(gameObject, 5f);
                }
            }           
        }

        protected virtual void InitPersonParameters()
        {
            _personAnimator = GetComponent<PersonAnimatorDragonBones>();
            _personAnimator.AnimationTimeScale = AnimationTimeScaleParameter.Value;
            HealthParameter.OnChangeValue.AddListener(health =>
            {
                if (health >= HealthParameter.MaxValue)
                {
                    health = HealthParameter.MaxValue;
                }
                HpProgressor.SetValue(health);
            });
            HpProgressor.SetMax(HealthParameter.MaxValue);
            HealthParameter.Value = HealthParameter.MaxValue;
        }

    }

    [System.Serializable]
    public class HitEvent : UnityEvent<float> { }
}

