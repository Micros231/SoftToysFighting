using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Plugins.xNodeUtilityAi.AbstractNodes;
using Plugins.xNodeUtilityAi.Framework;

namespace Com.SoftToysFighting.Person.Enemies.AI.Nodes
{
	[CreateNodeMenu("EnemyAI/Actions/MoveToPlayerAction")]
	public class MoveToPlayerAction : EnemyActionBase
	{
		protected override void ExecuteEnemyAction(EnemyAIComponent context, AIData aiData)
		{
			context.EnemyAgent.MoveToTarget();
		}
	}
}
