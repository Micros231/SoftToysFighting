using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.SoftToysFighting.Person.Enemies.AI.Nodes
{
    [CreateNodeMenu("EnemyAI/Entries/TargetPosition")]
    public class TargetPosition : EnemyEntryNodeBase
    {
        protected override int ValueProviderEnemy(EnemyAIComponent context)
        {
            if (context.EnemyAgent.TargetTransform != null)
            {
                Vector2 targetPosition = context.EnemyAgent.TargetTransform.position;
                Vector2 position = context.EnemyAgent.transform.position;
                float returnValue = Vector2.Distance(targetPosition, position);
                returnValue *= 100;
                return (int)returnValue;
            }
            else
                return 0;
            
        }
    }
}

