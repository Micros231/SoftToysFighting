using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Plugins.xNodeUtilityAi.Framework;

namespace Com.SoftToysFighting.Person.Enemies.AI
{
    public class EnemyAIComponent : AbstractAIComponent
    {
        [HideInInspector]
        public EnemyAIAgent EnemyAgent;
        private void Awake()
        {
            EnemyAgent = GetComponent<EnemyAIAgent>();
        }
    }
}

