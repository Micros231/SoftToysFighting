using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Plugins.xNodeUtilityAi.AbstractNodes;
using Plugins.xNodeUtilityAi.Framework;

namespace Com.SoftToysFighting.Person.Enemies.AI.Nodes
{
    public abstract class EnemyActionBase : SimpleActionNode
    {
        public override void Execute(AbstractAIComponent context, AIData aiData)
        {
            EnemyAIComponent enemyAIComponent = context as EnemyAIComponent;
            ExecuteEnemyAction(enemyAIComponent, aiData);
        }
        protected abstract void ExecuteEnemyAction(EnemyAIComponent context, AIData aiData);
    }
}

