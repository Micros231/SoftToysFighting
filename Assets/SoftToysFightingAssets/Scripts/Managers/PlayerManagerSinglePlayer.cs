using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Com.SoftToysFighting.Person;
using Com.SoftToysFighting.Person.Player;
using Com.SoftToysFighting.Settings;

using Doozy.Engine.Progress;

namespace Com.SoftToysFighting.Managers
{
    public class PlayerManagerSinglePlayer : PersonManagerBase<ProfileSettings>
    {
        #region Public Properties
        public GameObject Player => _player;
        public PersonSpawner PlayerSpawner
        {
            get => _playerSpawner;
            set => _playerSpawner = value;
        }
        public SceneManagerSinglePlayer SceneManger
        {
            get;
            set;
        }
        #endregion

        #region Protected Properties
        protected override List<ParameterFloat> Parameters => Settings.PlayerSettings.CurrentPlayer.GetParameters();
        #endregion

        #region Private SerializeFields
        [SerializeField] 
        private GameObject _player;
        [SerializeField]
        private GameObject _playerPrefab;
        [SerializeField]
        private PersonSpawner _playerSpawner;
        [SerializeField] 
        private Progressor _hpPlayerProgressor;
        [SerializeField] 
        private Progressor _energyPlayerProgressor;
        #endregion

        private void Start()
        {
            PersonMovement playerMovement = _player.GetComponent<PersonMovement>();
            playerMovement.SetEndZonePoints(EndZonePointMin, EndZonePointMax);
        }
        protected override void InitManager()
        {
            if (_playerPrefab == null)
            {
                _playerPrefab = Settings.PlayerSettings.CurrentPlayer.Prefab;
                PlayerParameters playerParameters = _playerPrefab.GetComponent<PlayerParameters>();
                InitParameters(playerParameters);
            }
            else
            {
                PlayerParameters playerParameters = _playerPrefab.GetComponent<PlayerParameters>();
                playerParameters.HpProgressor = _hpPlayerProgressor;
                playerParameters.EnergyProgressor = _energyPlayerProgressor;
            }
            
            _playerSpawner.PersonPrefab = _playerPrefab;
            _player = _playerSpawner.Spawn();
            if(_player.TryGetComponent(out PlayerParameters parameters))
            {
                parameters.DeadEvent += PlayerDeadHandler;
            }
        }

        private void PlayerDeadHandler(object sender, System.EventArgs e)
        {
            if (_player.TryGetComponent(out PlayerParameters parameters))
            {
                parameters.DeadEvent -= PlayerDeadHandler;
            }
            _player = null;
            SceneManger.OnLevelComplete.Invoke(false);
        }

        private void InitParameters(PlayerParameters playerParameters)
        {
            playerParameters.HealthParameter.MaxValue = Settings.PlayerSettings.CurrentPlayer.MaxHealth.Value;
            playerParameters.EnergyParameter.MaxValue = Settings.PlayerSettings.CurrentPlayer.MaxEnergy.Value;
            playerParameters.MoveSpeedParameter.Value = Settings.PlayerSettings.CurrentPlayer.MoveSpeed.Value;
            playerParameters.AnimationTimeScaleParameter.Value = Settings.PlayerSettings.CurrentPlayer.AnimationTimeScale.Value;
            playerParameters.DamageHandParameter.Value = Settings.PlayerSettings.CurrentPlayer.DamageHand.Value;
            playerParameters.DamageLegParameter.Value = Settings.PlayerSettings.CurrentPlayer.DamageLeg.Value;
            playerParameters.DamageSuperParameter.Value = Settings.PlayerSettings.CurrentPlayer.DamageSuper.Value;

            playerParameters.HpProgressor = _hpPlayerProgressor;
            playerParameters.EnergyProgressor = _energyPlayerProgressor;
        }
    }
}

