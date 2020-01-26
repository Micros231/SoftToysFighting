using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Com.SoftToysFighting.Settings
{
    [Serializable]
    public class Player : Entity
    {
        public ParameterFloat MaxHealth;
        public ParameterFloat MaxEnergy;
        public ParameterFloat MoveSpeed;
        public ParameterFloat AnimationTimeScale;
        public ParameterFloat DamageHand;
        public ParameterFloat DamageLeg;
        public ParameterFloat DamageSuper;
        public override List<ParameterFloat> GetParameters()
        {
            List<ParameterFloat> parameters = new List<ParameterFloat>();
            parameters.Add(MaxHealth);
            parameters.Add(MaxEnergy);
            parameters.Add(MoveSpeed);
            parameters.Add(AnimationTimeScale);
            parameters.Add(DamageHand);
            parameters.Add(DamageLeg);
            parameters.Add(DamageSuper);

            return parameters;
        }
    }
}
