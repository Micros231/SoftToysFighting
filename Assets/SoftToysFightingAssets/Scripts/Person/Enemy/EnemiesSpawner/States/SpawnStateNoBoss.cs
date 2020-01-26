using Com.SoftToysFighting.Settings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Com.SoftToysFighting.CreateAssetMenuHelper;

namespace Com.SoftToysFighting.Person.Enemies.Spawner
{
    [CreateAssetMenu(fileName = nameof(SpawnStateNoBoss), menuName = ENEMY_SPAWN_STATES_PATH + nameof(SpawnStateNoBoss))]
    public class SpawnStateNoBoss : SpawnStateBase
    {
        public override GameObject GetPrefabToSpawn(List<Enemy> availableEnemies, out AI.EnemyType enemyType)
        {
            int selectEnemyNumber = UnityEngine.Random.Range(0, availableEnemies.Count);
            Enemy enemy = availableEnemies[selectEnemyNumber];
            enemyType = AI.EnemyType.Enemy;
            GameObject enemyPrefab = GetEnemyPrefab(enemy, enemyType);
            return enemyPrefab;
        }
    }
}

