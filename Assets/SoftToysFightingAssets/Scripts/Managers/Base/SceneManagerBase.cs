using UnityEngine;
using UnityEngine.SceneManagement;

using Doozy.Engine.SceneManagement;

using Com.SoftToysFighting.LevelSystem;
using Com.SoftToysFighting.Settings;

namespace  Com.SoftToysFighting.Managers
{
    public abstract class SceneManagerBase<T> : ManagerBase<T> where T : SceneSettings
    {
        #region Public Properties
        public GameObject Environment => _environment;
        public SceneLoader SceneLoader => _sceneLoader;
        public LevelController LevelController => _levelController;
        public Vector2 EndZonePointMax => _endZonePointMax;
        public Vector2 EndZonePointMin => _endZonePointMin;
        #endregion

        #region Private SerializeFields
        [SerializeField] 
        private SceneLoader _sceneLoader;
        [SerializeField] 
        private GameObject _environment;
        [SerializeField]
        private LevelController _levelController;
        [SerializeField]
        private Vector2 _endZonePointMax;
        [SerializeField]
        private Vector2 _endZonePointMin;
        #endregion

        #region Public Methods
        public abstract void Restart();
        public abstract void LoadMainMenu();

        #endregion
        #region Protected Methods
        protected abstract LevelController GenerateLevel();
        protected abstract void InitComponents();
        protected override void InitManager()
        {
            LevelController generatedLevel = GenerateLevel();
            _levelController = generatedLevel;
            InitEndZonePoints();
            InitComponents();
        }

        #endregion
        #region Private Methods
        private void InitEndZonePoints()
        {
            var endZoneUp = _levelController.GetEndZone(EndZone.DirectionEndZone.Up).transform.position;
            var endZoneDown = _levelController.GetEndZone(EndZone.DirectionEndZone.Down).transform.position;
            var endZoneRight = _levelController.GetEndZone(EndZone.DirectionEndZone.Right).transform.position;
            var endZoneLeft = _levelController.GetEndZone(EndZone.DirectionEndZone.Left).transform.position;
            _endZonePointMax = new Vector2(endZoneRight.x, endZoneUp.y);
            _endZonePointMin = new Vector2(endZoneLeft.x, endZoneDown.y);
        }
        #endregion

    }

}

