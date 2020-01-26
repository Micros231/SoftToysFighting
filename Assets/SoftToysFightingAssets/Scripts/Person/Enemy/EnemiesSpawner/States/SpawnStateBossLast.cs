using Com.SoftToysFighting.Settings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Com.SoftToysFighting.CreateAssetMenuHelper;

namespace Com.SoftToysFighting.Person.Enemies.Spawner
{
    [CreateAssetMenu(fileName = nameof(SpawnStateBossLast), menuName = ENEMY_SPAWN_STATES_PATH + nameof(SpawnStateBossLast))]
    public class SpawnStateBossLast : SpawnStateBase
    {
        public override GameObject GetPrefabToSpawn(List<Enemy> availableEnemies, out AI.EnemyType enemyType)
        {
            int selectEnemyNumber = Random.Range(0, availableEnemies.Count);
            Enemy enemy = availableEnemies[selectEnemyNumber];
            GameObject enemyPrefab;
            if (RemaingAmountEnemiesInLevel == 1)
            {
                enemyPrefab = GetEnemyPrefab(enemy, AI.EnemyType.Boss);
                enemyType = AI.EnemyType.Boss;
            }
            else
            {
                enemyPrefab = GetEnemyPrefab(enemy, AI.EnemyType.Enemy);
                enemyType = AI.EnemyType.Enemy;
            }
            
            return enemyPrefab;
        }
    }
}

