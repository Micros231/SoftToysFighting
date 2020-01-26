using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.SoftToysFighting.Person.Enemies.AI.Nodes
{
    [CreateNodeMenu("EnemyAI/Entries/MaxPosition")]
    public class MaxPosition : EnemyEntryNodeBase
    {
        protected override int ValueProviderEnemy(EnemyAIComponent context)
        {
            if (context.EnemyAgent.TargetTransform != null)
            {
                Vector2 targetPosition = context.EnemyAgent.TargetTransform.position;
                Vector2 position = context.EnemyAgent.transform.position;
                Vector2 attackingZoneSize = context.EnemyAgent.AttackingZoneSize;
                float returnValue;
                if (targetPosition.x > position.x + attackingZoneSize.x)
                {
                    returnValue = Vector2.Distance(position + attackingZoneSize, position);
                }
                else if (targetPosition.x < position.x - attackingZoneSize.x)
                {
                    returnValue = Vector2.Distance(position - attackingZoneSize, position);
                }
                else if (targetPosition.x < position.x + attackingZoneSize.x && targetPosition.x > position.x)
                {
                    returnValue = Vector2.Distance(position + attackingZoneSize, position);
                }
                else if (targetPosition.x > position.x - attackingZoneSize.x && targetPosition.x < position.x)
                {
                    returnValue = Vector2.Distance(position + attackingZoneSize, position);
                }
                else
                {
                    returnValue = 0;
                }
                returnValue *= 100;
                return (int)returnValue;
            }
            else
                return 0;
           
        }
    }
}

