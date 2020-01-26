using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.SoftToysFighting.Person.Enemies.AI;

namespace Com.SoftToysFighting.Person.Enemies
{
    public class EnemyController : PersonController
    {
        private EnemyAIAgent EnemyAI { get; set; }

        protected override void InitPersonSystems()
        {
            base.InitPersonSystems();
            EnemyAI = GetComponent<EnemyAIAgent>();
        }

        protected override void Move()
        {
            PersonMovement.Move(EnemyAI.MoveToDirection, EnemyAI.IsJump);
        }
        protected override void Attack()
        {
            PersonAttacker.Attack(EnemyAI.IsAttackHand, EnemyAI.IsAttackLeg, EnemyAI.IsAttackSuper);
        }
    }
}

