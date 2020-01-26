using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Plugins.xNodeUtilityAi.AbstractNodes;
using Plugins.xNodeUtilityAi.Framework;

namespace Com.SoftToysFighting.Person.Enemies.AI.Nodes
{
    public abstract class EnemyEntryNodeBase : SimpleEntryNode
    {
        protected override int ValueProvider(AbstractAIComponent context)
        {
            EnemyAIComponent enemyAIComponent = context as EnemyAIComponent;
            return ValueProviderEnemy(enemyAIComponent);
        }

        protected abstract int ValueProviderEnemy(EnemyAIComponent context);

    }

}
