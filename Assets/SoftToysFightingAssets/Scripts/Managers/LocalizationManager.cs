using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

using Com.SoftToysFighting.Settings;
using Com.SoftToysFighting.DoozyUI;

using InfinityEngine.Localization;


namespace Com.SoftToysFighting.Managers
{
    public class LocalizationManager : ManagerBase<LocalizationSettings>
    {
        [SerializeField]
        private Image _imageLanguage;
        [SerializeField]
        private PopupShowerLanguageChoise _popupShower;

        protected override void InitManager()
        {
            ISILocalization.onLanguageChanged += OnLanguageChangedListener;
            ISILocalization.ChangeLanguage(Settings.Language);
            _popupShower.LocalizedLanguages = ISILocalization.Instance.LocalizedLanguages;
        }

        private void OnDestroy()
        {
            ISILocalization.onLanguageChanged -= OnLanguageChangedListener;
        }
        private void OnLanguageChangedListener()
        {
            LocalizedLanguage newLocalizedLanguage = ISILocalization.Instance.LocalizedLanguages.SingleOrDefault(language => language.Language == ISILocalization.CurrentLanguage);
            Texture2D flagTexture = newLocalizedLanguage.Flag;
            _imageLanguage.sprite = Sprite.Create(flagTexture, _imageLanguage.sprite.rect, _imageLanguage.sprite.pivot);
            Settings.Language = ISILocalization.CurrentLanguage;
            Settings.SaveSettings();
        }
    }
}

