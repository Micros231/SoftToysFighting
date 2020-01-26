using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Plugins.xNodeUtilityAi.AbstractNodes;
using Plugins.xNodeUtilityAi.Framework;

namespace Com.SoftToysFighting.Person.Enemies.AI.Nodes
{
    [CreateNodeMenu("EnemyAI/Entries/IsMoveable")]
    public class IsMoveable : EnemyEntryNodeBase
    {
        protected override int ValueProviderEnemy(EnemyAIComponent context)
        {
            return context.EnemyAgent.MoveToDirection == Vector2.zero ? 0 : 1;
        }
    }
}

