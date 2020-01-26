using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Com.SoftToysFighting.Settings;

namespace Com.SoftToysFighting.Person.Enemies.Spawner
{
    [Serializable]
    public class EnemySpawner
    {
        #region Public Properties
        public SpawnStateBase SpawnState 
        {
            get => _spawnState;
            set => _spawnState = value;      
        }
        public List<Enemy> EnemiesInSpawn => _enemiesInSpawn;
        #endregion

        #region Private SerializeFields

        [SerializeField]
        private SpawnStateBase _spawnState;
        [SerializeField]
        private List<PersonSpawner> _leftEnemySpawners = new List<PersonSpawner>();
        [SerializeField]
        private List<PersonSpawner> _rightEnemySpawners = new List<PersonSpawner>();
        [SerializeField]
        private List<Enemy> _enemiesInSpawn;
        #endregion

        #region Private Fields
        private PersonSpawner _lastUsedSpawner;
        private Vector2 _endZonePointMin;
        private Vector2 _endZonePointMax;
        private int _remaingAmountEnemiesInLevel;
        #endregion

        public void InitSpawner(
            SpawnStateBase spawnState,
            List<Enemy> enemiesInSpawn,
            Vector2 endZonePointMin, 
            Vector2 endZonePointMax,
            ref int remaingAmountEnemiesInLevel)
        {
            _spawnState = spawnState;
            _enemiesInSpawn = new List<Enemy>(enemiesInSpawn);
            _endZonePointMin = endZonePointMin;
            _endZonePointMax = endZonePointMax;
            _remaingAmountEnemiesInLevel = remaingAmountEnemiesInLevel;
        }
        public GameObject Spawn(out AI.EnemyType enemyType)
        {
            if (_spawnState == null)
            {
                throw new ArgumentNullException("_spawnState", "SpawnState is null");
            }
            _spawnState.RemaingAmountEnemiesInLevel = _remaingAmountEnemiesInLevel;
            GameObject enemyPrefab = _spawnState.GetPrefabToSpawn(GetAvailableEnemies(), out enemyType);
            PersonMovement enemyMovement = enemyPrefab.GetComponent<PersonMovement>();
            enemyMovement.SetEndZonePoints(_endZonePointMin, _endZonePointMax);
            return SelectAvailableSpawner().Spawn(enemyPrefab);
        }

        public PersonSpawner SelectAvailableSpawner()
        {
            List<PersonSpawner> availableEnemySpawners = new List<PersonSpawner>();
            foreach (var leftSpawner in _leftEnemySpawners)
            {
                if (leftSpawner.transform.position.x > _endZonePointMin.x)
                    availableEnemySpawners.Add(leftSpawner);
            }
            foreach (var rightSpawner in _rightEnemySpawners)
            {
                if (rightSpawner.transform.position.x < _endZonePointMax.x)
                    availableEnemySpawners.Add(rightSpawner);
            }
            int selectNumberSpawner;
            if (_lastUsedSpawner != null)
            {
                if (availableEnemySpawners.Contains(_lastUsedSpawner))
                {
                    availableEnemySpawners.Remove(_lastUsedSpawner);
                    selectNumberSpawner = UnityEngine.Random.Range(0, availableEnemySpawners.Count);
                }
                else
                {
                    selectNumberSpawner = UnityEngine.Random.Range(0, availableEnemySpawners.Count);
                }
            }
            else
            {
                selectNumberSpawner = UnityEngine.Random.Range(0, availableEnemySpawners.Count);
            }
            _lastUsedSpawner = availableEnemySpawners[selectNumberSpawner];
            return _lastUsedSpawner;
        }
        public List<Enemy> GetAvailableEnemies()
        {
            List<Enemy> availableEnemies = new List<Enemy>();
            if (_enemiesInSpawn == null)
            {
                throw new ArgumentNullException("_enemiesInSpawn", "EnemiesInSpawn is Null");
            }
            foreach (var enemy in _enemiesInSpawn)
            {
                if (enemy.IsAvailable == true)
                {
                    availableEnemies.Add(enemy);
                }
            }
            return availableEnemies;
        }
    }
}

