using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Com.SoftToysFighting.Settings
{
    [Serializable]
    public class Enemy : Entity
    {
        [Header("Enemy Parameters")]
        public ParameterFloat MaxHealthEnemy;
        public ParameterFloat MoveSpeedEnemy;
        public ParameterFloat TimeScaleAnimationEnemy;
        public ParameterFloat DamageHandEnemy;
        public ParameterFloat DamageLegEnemy;
        [Header("Boss Parameters")]
        public ParameterFloat MaxHealthBoss;
        public ParameterFloat MoveSpeedBoss;
        public ParameterFloat TimeScaleAnimationBoss;
        public ParameterFloat DamageHandBoss;
        public ParameterFloat DamageLegBoss;
        public ParameterFloat DamageSuperBoss;

        public override List<ParameterFloat> GetParameters()
        {
            List<ParameterFloat> parameters = new List<ParameterFloat>();
            parameters.Add(MaxHealthEnemy);
            parameters.Add(MoveSpeedEnemy);
            parameters.Add(TimeScaleAnimationEnemy);
            parameters.Add(DamageHandEnemy);
            parameters.Add(DamageLegEnemy);

            parameters.Add(MaxHealthBoss);
            parameters.Add(MoveSpeedBoss);
            parameters.Add(TimeScaleAnimationBoss);
            parameters.Add(DamageHandBoss);
            parameters.Add(DamageLegBoss);
            parameters.Add(DamageSuperBoss);
            return parameters;
        }
    }
}

