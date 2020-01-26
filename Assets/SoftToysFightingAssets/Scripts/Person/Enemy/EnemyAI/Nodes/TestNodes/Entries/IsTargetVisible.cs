using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Plugins.xNodeUtilityAi.AbstractNodes;
using Plugins.xNodeUtilityAi.Framework;

namespace Com.SoftToysFighting.Person.Enemies.AI.Nodes
{
	[CreateNodeMenu("EnemyAI/Entries/IsTargetVisible")]
	public class IsTargetVisible : EnemyEntryNodeBase
	{
		protected override int ValueProviderEnemy(EnemyAIComponent context)
		{
			return context.EnemyAgent.IsTargetVisible ? 1 : 0;
		}
	}
}
