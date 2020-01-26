using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine.Progress;
namespace Com.SoftToysFighting.Person.Player
{
    public class PlayerParameters : PersonParameters
    {
        public ParameterMaxFloat EnergyParameter = new ParameterMaxFloat();
        public Progressor EnergyProgressor;

        protected override void InitPersonParameters()
        {
            base.InitPersonParameters();          
            EnergyParameter.OnChangeValue.AddListener(energy =>
            {
                if (energy >= EnergyParameter.MaxValue)
                {
                    energy = EnergyParameter.MaxValue;
                }
                if (energy <= 0)
                {
                    energy = 0;
                }
                EnergyProgressor.SetValue(energy);
            });
            EnergyProgressor.SetMax(EnergyParameter.MaxValue);
            EnergyProgressor.SetValue(EnergyParameter.Value);
            HealthParameter.OnChangeValue.AddListener(health =>
            {
                EnergyParameter.Value += EnergyParameter.MaxValue / 10;
            });
        }
    }
}

