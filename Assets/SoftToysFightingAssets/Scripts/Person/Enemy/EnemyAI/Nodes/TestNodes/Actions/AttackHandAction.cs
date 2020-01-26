using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Plugins.xNodeUtilityAi.Framework;
using Plugins.xNodeUtilityAi.AbstractNodes;

namespace Com.SoftToysFighting.Person.Enemies.AI.Nodes
{
	[CreateNodeMenu("EnemyAI/Actions/AttackHandAction")]
	public class AttackHandAction : EnemyActionBase 
	{
		protected override void ExecuteEnemyAction(EnemyAIComponent context, AIData aiData)
		{
			context.EnemyAgent.AttackHand();
			context.EnemyAgent.IsAction = true;
		}
	}
}
