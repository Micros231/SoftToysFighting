using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Com.SoftToysFighting.Settings;
using Com.SoftToysFighting.DoozyUI;

using Doozy.Engine.SceneManagement;


namespace Com.SoftToysFighting.Managers
{
    public class LevelManager : ManagerBase<SceneSettingsSinglePlayer>
    {
        [SerializeField]
        private SceneLoader _sceneLoader;
        [SerializeField]
        private Transform _transformContainerLevels;
        [SerializeField]
        private GameObject _prefabLevelChooseObject;
        [SerializeField]
        private LevelPresentor _selectedLevel;
        [SerializeField]
        private List<LevelPresentor> _levelChoosePresentors;
        
        protected override void InitManager()
        {
            InstantiateLevels();
        }
        private void InstantiateLevels()
        {
            if (Settings.LevelSettings.Levels == null)
            {
                throw new ArgumentNullException("Levels", "Levels is null");
            }
            foreach (var level in Settings.LevelSettings.Levels)
            {
                LevelPresentor levelChoosePresentor = 
                    Instantiate(_prefabLevelChooseObject, _transformContainerLevels).GetComponent<LevelPresentor>();
                _levelChoosePresentors.Add(levelChoosePresentor);
                levelChoosePresentor.ImageBackground.sprite = level.LevelSprite;
                levelChoosePresentor.EntityName = level.Name;
                if (level.IsAvailable)
                {
                    levelChoosePresentor.Available();
                }
                else
                {
                    levelChoosePresentor.Unavailable();
                }
                levelChoosePresentor.ButtonPlay.OnClick.OnTrigger.Event.AddListener(() =>
                {
                    Debug.Log($"Play level {Settings.LevelSettings.CurrentLevel.Name}");
                    if (SceneManager.GetSceneByBuildIndex(Settings.LevelSettings.SceneBuildIndexToLoad) == null)
                        throw new ArgumentNullException("Scene", $"Not available scene to build index {Settings.LevelSettings.SceneBuildIndexToLoad}");
                    _sceneLoader.LoadSceneAsyncSingle(Settings.LevelSettings.SceneBuildIndexToLoad);

                });
                levelChoosePresentor.ButtonChooseLevel.OnClick.OnTrigger.Event.AddListener(() =>
                {
                    foreach (var levelChoose in _levelChoosePresentors)
                    {
                        if (levelChoose != levelChoosePresentor)
                        {
                            levelChoose.Deselect();
                        }
                    }
                    if (levelChoosePresentor.IsSelected)
                    {
                        levelChoosePresentor.Deselect();
                        _selectedLevel = null;
                        if (levelChoosePresentor.IsAvailable)
                        {
                            Settings.LevelSettings.CurrentLevel = null;
                        }
                    }
                    else
                    {
                        levelChoosePresentor.Select();
                        _selectedLevel = levelChoosePresentor;
                        if (levelChoosePresentor.IsAvailable)
                        {
                            Settings.LevelSettings.CurrentLevel = level;
                            Debug.Log(Settings.LevelSettings.CurrentLevel.Name);
                        }
                    }
                    Settings.SaveSettings();
                });
                if (level == Settings.LevelSettings.CurrentLevel)
                {
                    levelChoosePresentor.Select();
                }
            }
        }
    }
}

