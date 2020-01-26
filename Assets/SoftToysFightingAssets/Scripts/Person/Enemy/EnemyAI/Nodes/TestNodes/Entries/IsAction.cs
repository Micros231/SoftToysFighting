using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.SoftToysFighting.Person.Enemies.AI.Nodes
{
    [CreateNodeMenu("EnemyAI/Entries/IsAction")]
    public class IsAction : EnemyEntryNodeBase
    {
        protected override int ValueProviderEnemy(EnemyAIComponent context)
        {
            return context.EnemyAgent.IsAction ? 1 : 0;
        }
    }
}

