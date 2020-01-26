using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

using Com.SoftToysFighting.Settings;
using Com.SoftToysFighting.LevelSystem;
 


namespace  Com.SoftToysFighting.Managers
{
    public class SceneManagerSinglePlayer : SceneManagerBase<SceneSettingsSinglePlayer>
    {
        #region Public Fileds
        public CompleteLevelEvent OnLevelComplete;
        #endregion

        #region Private SerializeFields
        [SerializeField] private PlayerManagerSinglePlayer _playerManagerSinglePlayer;
        [SerializeField] private EnemiesManagerSinglePlayer _enemiesManagerSinglePlayer;
        [SerializeField] private CameraFollowToPerson _cameraFollowToPerson;
        [SerializeField] private bool _isGenerateLevelInSettings;
        #endregion

        #region MonoBehaviours Callbacks
        private void OnDestroy()
        {
            OnLevelComplete.RemoveListener(LevelCompleteHandler);
        }
        #endregion

        #region Public Methods
        public override void Restart()
        {
            SceneLoader.LoadSceneAsyncSingle(SceneManager.GetActiveScene().buildIndex);
        }

        public override void LoadMainMenu()
        {
            SceneLoader.LoadSceneAsyncSingle(0);
        }
        #endregion
        #region Protected Methods
        protected override void InitComponents()
        {
            InitPlayerManager();
            InitEnemiesManager();
            InitCameraFollow();
            OnLevelComplete.AddListener(LevelCompleteHandler);
        }
        protected override LevelController GenerateLevel()
        {
            if (_isGenerateLevelInSettings)
            {
                if (Environment.GetComponentInChildren<LevelController>() != null)
                {
                    Destroy(Environment.GetComponentInChildren<LevelController>().gameObject);
                }
                GameObject level = 
                    Instantiate(
                        Settings.LevelSettings.CurrentLevel.Prefab,
                        Environment.transform);
                LevelController levelController = level.GetComponent<LevelController>();
                if (levelController == null)
                    throw new ArgumentNullException("levelController", $"In {level.name} not available {nameof(levelController)} component");
                return levelController;
            }
            else
            {
                GameObject level = Environment.GetComponentInChildren<LevelController>().gameObject;
                LevelController levelController = level.GetComponent<LevelController>();
                if (levelController == null)
                    throw new ArgumentNullException("levelController", $"In {level.name} not available {nameof(levelController)} component");
                return levelController;
            }
            

        }
        #endregion

        #region Private Methods
        private void InitCameraFollow()
        {
            _cameraFollowToPerson.SetTansforms(
                _playerManagerSinglePlayer.Player.GetComponent<Transform>(),
                LevelController.GetEndZone(EndZone.DirectionEndZone.Left).transform,
                LevelController.GetEndZone(EndZone.DirectionEndZone.Right).transform);
        }
        private void InitPlayerManager()
        {
            _playerManagerSinglePlayer.SetEndZonePoints(EndZonePointMin, EndZonePointMax);
            _playerManagerSinglePlayer.SceneManger = this;
        }
        private void InitEnemiesManager()
        {
            _enemiesManagerSinglePlayer.Target = _playerManagerSinglePlayer.Player;
            _enemiesManagerSinglePlayer.SceneManger = this;
            _enemiesManagerSinglePlayer.SetEndZonePoints(EndZonePointMin, EndZonePointMax);
        }
        private void LevelCompleteHandler(bool isWin)
        {
            if (isWin)
            {
                Debug.Log("Win");
            }
            else
            {
                Debug.Log("Lose");
            }
            LoadMainMenu();
        }
        #endregion
    }
    [System.Serializable]
    public class CompleteLevelEvent : UnityEvent<bool> { }
}

