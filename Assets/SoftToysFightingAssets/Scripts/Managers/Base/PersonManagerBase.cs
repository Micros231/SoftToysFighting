using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Com.SoftToysFighting.Person;
using Com.SoftToysFighting.Settings;


namespace  Com.SoftToysFighting.Managers
{
    public abstract class PersonManagerBase<T> : ManagerBase<T> where T : SettingsBase
    {
        protected  abstract  List<ParameterFloat> Parameters { get; }
        protected Vector2 EndZonePointMax => _endZonePointMax;
        protected Vector2 EndZonePointMin => _endZonePointMin;

        [SerializeField]
        private Vector2 _endZonePointMax;
        [SerializeField]
        private Vector2 _endZonePointMin;

        protected ParameterFloat GetParameter(string name)
        {
            if (Parameters == null)
            {
                throw new ArgumentNullException("Parameters", "Parameters is null");
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name", "Input string 'name' is null or empty");
            }
            ParameterFloat parameterFloat = 
                Parameters.SingleOrDefault(parameter => parameter.Name.ToLower() == name.ToLower());
            if (parameterFloat == null)
            {
                throw new ArgumentNullException("parameterFloat", $"ParameterFloat don't find in {name}");
            }
            return parameterFloat;
        } 

        public void SetEndZonePoints(Vector2 minPoint, Vector2 maxPoint)
        {
            _endZonePointMin = minPoint;
            _endZonePointMax = maxPoint;
        }

    }
}

