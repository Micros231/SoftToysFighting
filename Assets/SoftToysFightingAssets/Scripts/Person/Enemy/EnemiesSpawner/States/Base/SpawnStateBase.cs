using Com.SoftToysFighting.Settings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Com.SoftToysFighting.Person.Enemies.AI;

namespace Com.SoftToysFighting.Person.Enemies.Spawner
{
    public abstract class SpawnStateBase : ScriptableObject
    {
        public int RemaingAmountEnemiesInLevel { protected get; set; }
        public abstract GameObject GetPrefabToSpawn(List<Enemy> availableEnemies, out AI.EnemyType enemyType);

        public GameObject GetEnemyPrefab(Enemy enemy, AI.EnemyType enemyType)
        {
            GameObject enemyPrefab = enemy.Prefab;
            PersonParameters enemyParameters = enemyPrefab.GetComponent<PersonParameters>();
            enemyParameters = SetParameters(enemyParameters, enemy, enemyType);
            return enemyPrefab;
        }
        private PersonParameters SetParameters(PersonParameters enemyParameters, Enemy enemy, AI.EnemyType enemyType)
        {
            switch (enemyType)
            {
                case AI.EnemyType.Enemy:
                    enemyParameters.HealthParameter.MaxValue = enemy.MaxHealthEnemy.Value;
                    enemyParameters.MoveSpeedParameter.Value = enemy.MoveSpeedEnemy.Value;
                    enemyParameters.AnimationTimeScaleParameter.Value = enemy.TimeScaleAnimationEnemy.Value;
                    enemyParameters.DamageHandParameter.Value = enemy.DamageHandEnemy.Value;
                    enemyParameters.DamageLegParameter.Value = enemy.DamageLegEnemy.Value;
                    break;
                case AI.EnemyType.Boss:
                    enemyParameters.HealthParameter.MaxValue = enemy.MaxHealthBoss.Value;
                    enemyParameters.MoveSpeedParameter.Value = enemy.MoveSpeedBoss.Value;
                    enemyParameters.AnimationTimeScaleParameter.Value = enemy.TimeScaleAnimationBoss.Value;
                    enemyParameters.DamageHandParameter.Value = enemy.DamageHandBoss.Value;
                    enemyParameters.DamageLegParameter.Value = enemy.DamageLegBoss.Value;
                    enemyParameters.DamageLegParameter.Value = enemy.DamageSuperBoss.Value;
                    break;
            }
            return enemyParameters;
        }
    }
}

