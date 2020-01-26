using UnityEngine;
using Com.SoftToysFighting.Settings;

namespace Com.SoftToysFighting.Managers
{
    public abstract class ManagerBase<T> : MonoBehaviour where T : SettingsBase
    {
        protected T Settings { get; set; }
        private void Awake()
        {
            LoadSettings();
            InitManager();
            SaveSettings();
        }
        protected abstract void InitManager();
        public void UnloadSettings()
        {
            Resources.UnloadAsset(Settings);
        }
        private void LoadSettings()
        {
            Settings = SettingsBase.GetSettings<T>();
            Settings.LoadSettings();
        }
        private void SaveSettings()
        {
            Settings.SaveSettings();
        }  
    }
}

