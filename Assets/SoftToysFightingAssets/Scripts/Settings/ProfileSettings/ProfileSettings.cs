using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Com.SoftToysFighting.CreateAssetMenuHelper;


namespace Com.SoftToysFighting.Settings
{
    [CreateAssetMenu(fileName = nameof(ProfileSettings), menuName = SETTINGS_PATH + nameof(ProfileSettings))]
    public class ProfileSettings : SettingsBase
    {
        public string NickName;
        public int Batteries;
        public int Level;
        public float Experience;

        public PlayerSettings PlayerSettings;

        public override void LoadSettings()
        {
            Debug.Log("Load Profile Settings");
        }

        public override void SaveSettings()
        {
            Debug.Log("Save Profile Settings");
        }
    }

}

