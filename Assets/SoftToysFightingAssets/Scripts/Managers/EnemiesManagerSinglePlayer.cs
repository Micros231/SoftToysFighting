using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Com.SoftToysFighting.Settings;
using Com.SoftToysFighting.Person;
using Com.SoftToysFighting.Person.Enemies;
using Com.SoftToysFighting.Person.Enemies.AI;
using Com.SoftToysFighting.Person.Enemies.Spawner;

namespace  Com.SoftToysFighting.Managers
{

    public class EnemiesManagerSinglePlayer : PersonManagerBase<SceneSettingsSinglePlayer>
    {

        #region Public Properties
        public GameObject Target
        {
            get => _target;
            set => _target = value;
        }
        public SceneManagerSinglePlayer SceneManger   
        {
            get;
            set;
        }
        public GameObject LastEnemySpawn => _lastEnemySpawn;
        public int AmountEnemiesInLevel => _amountEnemiesInLevel;
        public int RemaingAmountEnemiesInLevel
        {
            get => _remaingAmountEnemiesInLevel;
            private set
            {
                _remaingAmountEnemiesInLevel = value;
                if (_remaingAmountEnemiesInLevel >= AmountEnemiesInLevel)
                    _remaingAmountEnemiesInLevel = AmountEnemiesInLevel;
                if (_remaingAmountEnemiesInLevel <= 0)
                    _remaingAmountEnemiesInLevel = 0;
            } 
        }
        #endregion

        #region Private SerializeFields
        [SerializeField]
        private bool _isInitInSettings; 
        [SerializeField] 
        private GameObject _target;
        [SerializeField]
        private EnemySpawner _enemySpawner;
        [SerializeField]
        private int _amountEnemiesInLevel;
        [SerializeField]
        private int _remaingAmountEnemiesInLevel;
        [SerializeField]
        private int _maxEnemiesAliveInLevel;
        [SerializeField] 
        private float _timeToRepeatUpdateAttacking;
        [SerializeField] 
        private float _timeToRepeatSpawnEnemy;
        [SerializeField]
        private GameObject _lastEnemySpawn;
        #endregion

        #region Private Properties
        private List<EnemyAIAgent> EnemyAgents => Enemies.Select((enemy) => enemy.GetComponent<EnemyAIAgent>()).ToList();

        protected override List<ParameterFloat> Parameters => null;
        #endregion

        #region Private Fields
        private List<GameObject> Enemies = new List<GameObject>();
        private float _timerToEnemySpawn;
        private PersonSpawner _lastUsedSpawner;
        #endregion

        


        #region MonoBehaviour Callbacks
        private void Start()
        {
            InvokeRepeating(nameof(UpdateEnemiesAttack), 0, _timeToRepeatUpdateAttacking);
        }
        private void Update()
        {
            UpdateSpawnEnemy();
        }
        #endregion

        #region Public Methods
        public void AddEnemy(GameObject enemy)
        {
            AddEnemyDeadHandler(enemy);
            Enemies.Add(enemy);
        }
        public void RemoveEnemy(GameObject enemy)
        {
            Enemies.Remove(enemy);
            RemoveEnemyDeadHandler(enemy);
            RemaingAmountEnemiesInLevel--;
            if (RemaingAmountEnemiesInLevel == 0)
            {
                SceneManger.OnLevelComplete.Invoke(true);
            }
        }
        #endregion

        #region Protected Methods

        protected override void InitManager()
        {
            if (_isInitInSettings)
            {
                _amountEnemiesInLevel = Settings.LevelSettings.CurrentLevel.EnemySettingsInLevel.AmountEnemiesInLevel;
                _maxEnemiesAliveInLevel = Settings.LevelSettings.CurrentLevel.EnemySettingsInLevel.MaxEnemiesAliveInLevel;
            }

            RemaingAmountEnemiesInLevel = AmountEnemiesInLevel;

            List<Enemy> enemiesInSpawn = _isInitInSettings ?
                new List<Enemy>(Settings.LevelSettings.CurrentLevel.EnemySettingsInLevel.Enemies) :
                new List<Enemy>(_enemySpawner.EnemiesInSpawn);

            SpawnStateBase spawnState = _isInitInSettings ?
                Settings.LevelSettings.CurrentLevel.EnemySettingsInLevel.SpawnState :
                _enemySpawner.SpawnState;

            _enemySpawner.InitSpawner(
                spawnState,
                enemiesInSpawn,
                EndZonePointMin, 
                EndZonePointMax, 
                ref _remaingAmountEnemiesInLevel);

        }
        #endregion

        #region Private Methods
        private void UpdateEnemiesAttack()
        {
            /*if (Enemies.Count > 1)
            {
                int randNumberEnemyiesAttack = UnityEngine.Random.Range(0, EnemyAgents.Count);
                for (int i = 0; i < EnemyAgents.Count; i++)
                {
                    if (i == randNumberEnemyiesAttack)
                    {
                        EnemyAgents[i].IsEnemiesAttacking = false;
                        continue;
                    }
                    EnemyAgents[i].IsEnemiesAttacking = true;
                }
            }
            else if (Enemies.Count < 2)
            {
                for (int i = 0; i < EnemyAgents.Count; i++)
                {
                    EnemyAgents[i].IsEnemiesAttacking = false;
                }
            }
            else if (Enemies == null)
            {
                Debug.Log($"Enemies is null");
            }*/
        }
        private void UpdateSpawnEnemy()
        {
            if (RemaingAmountEnemiesInLevel > 0)
            {
                _timerToEnemySpawn += Time.deltaTime;
                if (Enemies.Count == 0 || (_timerToEnemySpawn >= _timeToRepeatSpawnEnemy && Enemies.Count() < _maxEnemiesAliveInLevel))
                {
                    Person.Enemies.AI.EnemyType enemyType;
                    GameObject enemy = _enemySpawner.Spawn(out enemyType);
                    PersonMovement enemyMovement = enemy.GetComponent<PersonMovement>();
                    EnemyAIAgent enemyAgent = enemy.GetComponent<EnemyAIAgent>();
                    enemyAgent.InitAIAgent(
                        Target.transform,
                        enemyType,
                        EndZonePointMin + enemyMovement.ColliderSize,
                        EndZonePointMax - enemyMovement.ColliderSize);
                    _lastEnemySpawn = enemy;
                    AddEnemy(enemy);
                    _timerToEnemySpawn = 0;
                }              
            }
        }
        private void EnemiesController_DeadEvent(object sender, System.EventArgs e)
        {
            Component component = sender as Component;
            RemoveEnemy(component.gameObject);
        }
        private void AddEnemyDeadHandler(GameObject enemy)
        {
            enemy.GetComponent<PersonParameters>().DeadEvent += EnemiesController_DeadEvent;
        }
        private void RemoveEnemyDeadHandler(GameObject enemy)
        {
            enemy.GetComponent<PersonParameters>().DeadEvent -= EnemiesController_DeadEvent;
        }
        #endregion
    }
}

