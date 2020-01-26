using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Com.SoftToysFighting.CreateAssetMenuHelper;
namespace Com.SoftToysFighting.Settings
{
    [CreateAssetMenu(fileName = nameof(SceneSettingsSinglePlayer), menuName = SETTINGS_PATH + nameof(SceneSettingsSinglePlayer))]
    public class SceneSettingsSinglePlayer : SceneSettings
    {
        public override void LoadSettings()
        {
            Debug.Log("Load Scene Settings Single Player");
        }

        public override void SaveSettings()
        {
            Debug.Log("Save Scene Settings Single Player");
        }
    }
}

