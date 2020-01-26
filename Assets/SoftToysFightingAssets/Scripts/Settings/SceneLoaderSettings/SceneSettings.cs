using UnityEngine;
namespace Com.SoftToysFighting.Settings
{
    public abstract class SceneSettings : SettingsBase
    {
        [Header("Level Settings")]
        public LevelSettings LevelSettings;
    }
}

