using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.SoftToysFighting.Person.Enemies.AI.Nodes
{
    [CreateNodeMenu("EnemyAI/Entries/IsTargetnInAttackingZone")]
    public class IsTargetInAttackingZone : EnemyEntryNodeBase
    {
        protected override int ValueProviderEnemy(EnemyAIComponent context)
        {
            Vector2 targetPosition;
            if (context.EnemyAgent.TargetTransform != null)
            {
                targetPosition = context.EnemyAgent.TargetTransform.position;
                Vector2 position = context.EnemyAgent.transform.position;
                Vector2 attackingZoneSize = context.EnemyAgent.AttackingZoneSize;
                if (targetPosition.x < position.x + attackingZoneSize.x &&
                targetPosition.x > position.x - attackingZoneSize.x)
                {
                    if (targetPosition.y < position.y + attackingZoneSize.y &&
                        targetPosition.y > position.y - attackingZoneSize.y)
                    {
                        context.EnemyAgent.UpdateFlip();
                        return 1;
                    }
                    else
                        return 0;
                }
                else
                    return 0;
            }
            else
            {
                context.EnemyAgent.Reload();
                return 0;
            }
            
            
        }
    }
}

