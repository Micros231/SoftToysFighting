using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Com.SoftToysFighting.Settings;
using static Com.SoftToysFighting.CreateAssetMenuHelper;

namespace Com.SoftToysFighting.Person.Enemies.Spawner
{
    [CreateAssetMenu(fileName = nameof(SpawnStateStandart), menuName = ENEMY_SPAWN_STATES_PATH + nameof(SpawnStateStandart))]
    public class SpawnStateStandart : SpawnStateBase
    {
        public override GameObject GetPrefabToSpawn(List<Enemy> availableEnemies, out AI.EnemyType enemyType)
        {
            int selectEnemyNumber = UnityEngine.Random.Range(0, availableEnemies.Count);
            Enemy enemy = availableEnemies[selectEnemyNumber];
            int selectEnemyTypeNumber = UnityEngine.Random.Range(0, 2);
            enemyType = (AI.EnemyType)selectEnemyTypeNumber;
            GameObject enemyPrefab = GetEnemyPrefab(enemy, enemyType);
            return enemyPrefab;
        }
    }
}

