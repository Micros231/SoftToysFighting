using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Com.SoftToysFighting.Settings
{
    public abstract class Entity
    {
        public string Name;
        public bool IsAvailable;
        public GameObject Prefab;

        public abstract List<ParameterFloat> GetParameters();
        
    }
}
