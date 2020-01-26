using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.SceneManagement;

using TMPro;

using Doozy.Engine.SceneManagement;

using Com.SoftToysFighting.Person;
using Com.SoftToysFighting.Settings;

namespace Com.SoftToysFighting.DoozyUI.Prototype
{
    public class SceneLoaderSinglePlayer : MonoBehaviour
    {
        #region Private SerializeFields
        
        [SerializeField]
        private TMP_Dropdown _dropdownSelectPlayer;
        [SerializeField]
        private TMP_Dropdown _dropdownSelectLevel;
        [SerializeField]
        private GameObject _playerSettingsPanel;
        [SerializeField]
        private GameObject _enemyCollectionPanel;
        [SerializeField]
        private GameObject _enemySettingsPanel;
        [SerializeField]
        private GameObject _inputSettingPrefab;
        [SerializeField]
        private GameObject _toggleSettingPrefab;
        #endregion

        #region Private Fields
        private SceneLoader _sceneLoader;
        private SceneSettingsSinglePlayer _sceneSettings;
        #endregion


        #region Monobehaviour Callbacks
        private void Awake()
        {
            //_sceneSettings = SettingsBase.LoadSettings("SceneLoaderSinglePlayerSettings") as SceneSettingsSinglePlayer;
            _sceneLoader = GameObject.FindWithTag("SceneLoader").GetComponent<SceneLoader>();
            InitDropdownOptions();
            //InitPlayerSettingsPanel();
            //InitEnemyCollectionPanel();
            InitEnemySettingsPanel();
        }
        #endregion

        #region Public Methods
        public void LoadLevel()
        {
            /*_sceneSettings.PlayerSetting.CurrentPlayer = _sceneSettings.PlayerSetting.Players[_dropdownSelectPlayer.value];
            _sceneSettings.LevelSettings.CurrentLevel = _sceneSettings.LevelSettings.Levels[_dropdownSelectLevel.value];
            if (SceneManager.GetSceneByBuildIndex(_sceneSettings.LevelSettings.SceneBuildIndexToLoad) == null) return;
            _sceneLoader.LoadSceneAsyncSingle(_sceneSettings.LevelSettings.SceneBuildIndexToLoad);
            return;*/
        }
        #endregion

        #region Private Methods
        private void InitDropdownOptions()
        {
            /*ClearDropdownOptions();
            _dropdownSelectPlayer.AddOptions(InitDropdownOption(_sceneSettings.PlayerSetting.Players.ToArray()));
            _dropdownSelectLevel.AddOptions(InitDropdownOption(_sceneSettings.LevelSettings.Levels.ToArray()));
            if (_sceneSettings.PlayerSetting.CurrentPlayer != null)
            {
                _dropdownSelectPlayer.value = _sceneSettings.PlayerSetting.Players.IndexOf(_sceneSettings.PlayerSetting.CurrentPlayer);
            }
            if (_sceneSettings.LevelSettings.CurrentLevel != null)
            {
                _dropdownSelectLevel.value = _sceneSettings.LevelSettings.Levels.IndexOf(_sceneSettings.LevelSettings.CurrentLevel);
            }
            DropdownOnValueChanged(_dropdownSelectPlayer, _sceneSettings.PlayerSetting.Players);
            DropdownOnValueChanged(_dropdownSelectLevel, _sceneSettings.LevelSettings.Levels);

        }       
        private void InitPlayerSettingsPanel()
        {
            foreach (var parameter in _sceneSettings.PlayerSetting.PlayerParameterSettings)
            {
                CreateInputFieldParameterSetting(_playerSettingsPanel.transform, parameter);
            } 
        }
        private void InitEnemyCollectionPanel()
        {
            /*foreach (var enemy in _sceneSettings.EnemySetting.Enemies)
            {
                ToggleSetting toggleSetting = CreateToggleSetting(_enemyCollectionPanel.transform, enemy.Name);
                toggleSetting.Toggle.isOn = enemy.IsAvailable;
                toggleSetting.Toggle.onValueChanged.AddListener(isAvailable =>
                {
                    enemy.IsAvailable = isAvailable;
                });
            }*/
        }
        private void InitEnemySettingsPanel()
        {
            /*InputFieldSetting countEnemiesInLevelInput =
                CreateInputField(
                    _enemySettingsPanel.transform,
                    "Count Enemies:",
                    TMP_InputField.ContentType.IntegerNumber);
            countEnemiesInLevelInput.Input.text = _sceneSettings.EnemySetting.AmountEnemiesInLevel.ToString();
            countEnemiesInLevelInput.Input.onValueChanged.AddListener(text =>
            {
                text = UpdateInputSetting(text, ref _sceneSettings.EnemySetting.AmountEnemiesInLevel);
            });
            foreach (var parameter in _sceneSettings.EnemySetting.EnemyParametersSettings)
            {
                CreateInputFieldParameterSetting(_enemySettingsPanel.transform, parameter);
            }*/

        }
        
        private void ClearDropdownOptions()
        {
            _dropdownSelectPlayer.ClearOptions();
            _dropdownSelectLevel.ClearOptions();
        }
        private List<TMP_Dropdown.OptionData> InitDropdownOption(Entity[] entities)
        {
            List<TMP_Dropdown.OptionData> optionDatas = new List<TMP_Dropdown.OptionData>();

            foreach (var entity in entities)
            {
                optionDatas.Add(new TMP_Dropdown.OptionData(entity.Name));
            }

            return optionDatas;
        }
        private void DropdownOnValueChanged<T>(TMP_Dropdown dropdown, List<T> collectons) where T : Entity
        {
            int lastValue = dropdown.value;
            dropdown.onValueChanged.AddListener(number =>
            {
                if (collectons.ElementAt(number).IsAvailable == false)
                {
                    dropdown.value = lastValue;
                }
                else
                {
                    dropdown.value = number;
                    lastValue = number;
                }
                Debug.Log($"OnValueChanged Number - {number} | Value - {dropdown.value} | LastValue = {lastValue}");
            });

        }

        private string UpdateInputSetting(string text, ref int value)
        {
            if (string.IsNullOrEmpty(text))
            {
                text = "0";
            }
            value = int.Parse(text);
            return text;
        }
        private void CreateInputFieldParameterSetting(Transform parent, ParameterFloat parameter)
        {
            InputFieldSetting inputFieldSetting = 
                CreateInputField(parent, parameter.Name, TMP_InputField.ContentType.DecimalNumber);
            inputFieldSetting.Input.text = parameter.Value.ToString();
            inputFieldSetting.Input.onValueChanged.AddListener(text =>
            {
                if (string.IsNullOrEmpty(text))
                    text = "0";
                parameter.Value = float.Parse(text);
            });
        }
        private InputFieldSetting CreateInputField(Transform parent, string name, TMP_InputField.ContentType contentType)
        {
            GameObject inputFieldObject = Instantiate(_inputSettingPrefab, parent);
            InputFieldSetting inputFieldSetting = inputFieldObject.GetComponent<InputFieldSetting>();
            inputFieldSetting.Name.text = name;
            inputFieldSetting.Input.contentType = contentType;
            return inputFieldSetting;
        }
        private ToggleSetting CreateToggleSetting(Transform parent, string name)
        {
            GameObject toggleObject = Instantiate(_toggleSettingPrefab, parent);
            ToggleSetting toggleSetting = toggleObject.GetComponent<ToggleSetting>();
            toggleSetting.Name.text = name;
            return toggleSetting;
        }
        #endregion
    }
}

#region Legacy
/*InputFieldSetting HpPlayerInput =  
                CreateInputField(
                    _playerSettingsPanel.transform, 
                    "Health Player:",
                    TMP_InputField.ContentType.IntegerNumber);
            HpPlayerInput.Input.text = _sceneLoaderSettings.PlayerSetting.HpPlayer.ToString();
            HpPlayerInput.Input.onValueChanged.AddListener(text =>
            {
                text = UpdateInputSetting(text, ref _sceneLoaderSettings.PlayerSetting.HpPlayer);
            });
            InputFieldSetting EnergyPlayerInput =
                CreateInputField(
                    _playerSettingsPanel.transform,
                    "Energy Player:",
                    TMP_InputField.ContentType.DecimalNumber);
            EnergyPlayerInput.Input.text = _sceneLoaderSettings.PlayerSetting.EnergyPlayer.ToString();
            EnergyPlayerInput.Input.onValueChanged.AddListener(text =>
            {
                text = UpdateInputSetting(text, ref _sceneLoaderSettings.PlayerSetting.EnergyPlayer);
            });
            InputFieldSetting DamageHandPlayerInput =
                CreateInputField(
                    _playerSettingsPanel.transform,
                    "Damage Hand:",
                    TMP_InputField.ContentType.DecimalNumber);
            DamageHandPlayerInput.Input.text = _sceneLoaderSettings.PlayerSetting.DamageHand.ToString();
            DamageHandPlayerInput.Input.onValueChanged.AddListener(text =>
            {
                text = UpdateInputSetting(text, ref _sceneLoaderSettings.PlayerSetting.DamageHand);
            });
            InputFieldSetting DamageLegPlayerInput =
                CreateInputField(
                    _playerSettingsPanel.transform,
                    "Damage Leg:",
                    TMP_InputField.ContentType.DecimalNumber);
            DamageLegPlayerInput.Input.text = _sceneLoaderSettings.PlayerSetting.DamageLeg.ToString();
            DamageLegPlayerInput.Input.onValueChanged.AddListener(text =>
            {
                text = UpdateInputSetting(text, ref _sceneLoaderSettings.PlayerSetting.DamageLeg);
            });
            InputFieldSetting DamageSuperPlayerInput =
                CreateInputField(
                    _playerSettingsPanel.transform,
                    "Damage Super:",
                    TMP_InputField.ContentType.DecimalNumber);
            DamageSuperPlayerInput.Input.text = _sceneLoaderSettings.PlayerSetting.DamageSuper.ToString();
            DamageSuperPlayerInput.Input.onValueChanged.AddListener(text =>
            {
                text = UpdateInputSetting(text, ref _sceneLoaderSettings.PlayerSetting.DamageSuper);
            });*/
/*InputFieldSetting HpNormalEnemyInput =
    CreateInputField(
        _enemySettingsPanel.transform,
        "Hp Enemy:",
        TMP_InputField.ContentType.IntegerNumber);
HpNormalEnemyInput.Input.text = _sceneLoaderSettings.EnemySetting.HpNormalNPC.ToString();
HpNormalEnemyInput.Input.onValueChanged.AddListener(text =>
{
    text = UpdateInputSetting(text, ref _sceneLoaderSettings.EnemySetting.HpNormalNPC);
});

InputFieldSetting DamageHandEnemyInput =
    CreateInputField(
        _enemySettingsPanel.transform,
        "Damage Hand Enemy:",
        TMP_InputField.ContentType.DecimalNumber);
DamageHandEnemyInput.Input.text = _sceneLoaderSettings.EnemySetting.DamageHandNormalNPC.ToString();
DamageHandEnemyInput.Input.onValueChanged.AddListener(text =>
{
    text = UpdateInputSetting(text, ref _sceneLoaderSettings.EnemySetting.DamageHandNormalNPC);
});

InputFieldSetting DamageLegEnemyInput =
    CreateInputField(
        _enemySettingsPanel.transform,
        "Damage Leg Enemy:",
        TMP_InputField.ContentType.DecimalNumber);
DamageLegEnemyInput.Input.text = _sceneLoaderSettings.EnemySetting.DamageLegNormalNPC.ToString();
DamageLegEnemyInput.Input.onValueChanged.AddListener(text =>
{
    text = UpdateInputSetting(text, ref _sceneLoaderSettings.EnemySetting.DamageLegNormalNPC);
});

InputFieldSetting DamageSuperEnemyInput =
    CreateInputField(
        _enemySettingsPanel.transform,
        "Damage Super Enemy:",
        TMP_InputField.ContentType.DecimalNumber);
DamageSuperEnemyInput.Input.text = _sceneLoaderSettings.EnemySetting.DamageSuperNormalNPC.ToString();
DamageSuperEnemyInput.Input.onValueChanged.AddListener(text =>
{
    text = UpdateInputSetting(text, ref _sceneLoaderSettings.EnemySetting.DamageSuperNormalNPC);
});

InputFieldSetting HpNormalBossInput =
    CreateInputField(
        _enemySettingsPanel.transform,
        "Hp Boss:",
        TMP_InputField.ContentType.IntegerNumber);
HpNormalBossInput.Input.text = _sceneLoaderSettings.EnemySetting.HpBossNPC.ToString();
HpNormalBossInput.Input.onValueChanged.AddListener(text =>
{
    text = UpdateInputSetting(text, ref _sceneLoaderSettings.EnemySetting.HpBossNPC);
});

InputFieldSetting DamageHandBossInput =
    CreateInputField(
        _enemySettingsPanel.transform,
        "Damage Hand Boss:",
        TMP_InputField.ContentType.DecimalNumber);
DamageHandBossInput.Input.text = _sceneLoaderSettings.EnemySetting.DamageHandBossNPC.ToString();
DamageHandBossInput.Input.onValueChanged.AddListener(text =>
{
    text = UpdateInputSetting(text, ref _sceneLoaderSettings.EnemySetting.DamageHandBossNPC);
});

InputFieldSetting DamageLegBossInput =
    CreateInputField(
        _enemySettingsPanel.transform,
        "Damage Leg Boss:",
        TMP_InputField.ContentType.DecimalNumber);
DamageLegBossInput.Input.text = _sceneLoaderSettings.EnemySetting.DamageLegBossNPC.ToString();
DamageLegBossInput.Input.onValueChanged.AddListener(text =>
{
    text = UpdateInputSetting(text, ref _sceneLoaderSettings.EnemySetting.DamageLegBossNPC);
});

InputFieldSetting DamageSuperBossInput =
    CreateInputField(
        _enemySettingsPanel.transform,
        "Damage Super Boss:",
        TMP_InputField.ContentType.DecimalNumber);
DamageSuperBossInput.Input.text = _sceneLoaderSettings.EnemySetting.DamageSuperBossNPC.ToString();
DamageSuperBossInput.Input.onValueChanged.AddListener(text =>
{
    text = UpdateInputSetting(text, ref _sceneLoaderSettings.EnemySetting.DamageSuperBossNPC);
});*/
#endregion