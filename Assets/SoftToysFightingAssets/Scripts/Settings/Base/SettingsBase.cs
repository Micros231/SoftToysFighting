using System;
using UnityEngine;


namespace  Com.SoftToysFighting.Settings
{ 
    public abstract class SettingsBase : ScriptableObject
    {
        public const string PATH_SETTINGS = "Settings/";

        public abstract void LoadSettings();

        public abstract void SaveSettings();

        public static T GetSettings<T>() where T : SettingsBase
        {
            string nameSettings = typeof(T).Name;
            var settingsResource = Resources.Load<T>($"{PATH_SETTINGS}{nameSettings}");
            if (settingsResource == null)
            {
                throw new ArgumentNullException("settingsResource", $"{nameSettings} is not found in Resources/{PATH_SETTINGS}");
            }
            return settingsResource;
        }
    }
}

