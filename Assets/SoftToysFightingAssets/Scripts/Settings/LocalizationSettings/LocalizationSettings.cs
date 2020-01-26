using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Com.SoftToysFighting.CreateAssetMenuHelper;

using InfinityEngine.Localization;

namespace Com.SoftToysFighting.Settings
{
    [CreateAssetMenu(fileName = nameof(LocalizationSettings), menuName = SETTINGS_PATH + nameof(LocalizationSettings))]
    public class LocalizationSettings : SettingsBase
    {
        public Language Language;

        public override void LoadSettings()
        {
            Debug.Log("Save Language");
        }

        public override void SaveSettings()
        {
            Debug.Log($"Save Language {Language}");
        }
    }
}

