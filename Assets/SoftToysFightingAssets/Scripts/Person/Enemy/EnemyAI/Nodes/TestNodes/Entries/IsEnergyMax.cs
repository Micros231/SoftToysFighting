using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.SoftToysFighting.Person.Enemies.AI.Nodes
{
    [CreateNodeMenu("EnemyAI/Entries/IsEnergyMax")]
    public class IsEnergyMax : EnemyEntryNodeBase
    {
        protected override int ValueProviderEnemy(EnemyAIComponent context)
        {
            return context.EnemyAgent.Energy >= context.EnemyAgent.MaxEnergy ? 1 : 0;
        }
    }
}

