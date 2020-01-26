using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.SoftToysFighting.Localization;
using Doozy.Engine.UI;
using InfinityEngine.Localization;

namespace Com.SoftToysFighting.DoozyUI
{
    public class PopupShowerLanguageChoise : PopupShowerBase
    {
        public List<LocalizedLanguage> LocalizedLanguages 
        { 
            get => _localizedLanguages; 
            set => _localizedLanguages = value; 
        }

        [SerializeField]
        private string _namePopup;
        [SerializeField]
        private GameObject _prefabLanguageCnangeButton;

        private List<LocalizedLanguage> _localizedLanguages = new List<LocalizedLanguage>();
        public override void ShowPopup()
        {
            UIPopup popup = UIPopup.GetPopup(_namePopup);
            popup.Show();
            if (_localizedLanguages != null)
            {
                foreach (var localizedLanguage in _localizedLanguages)
                {
                    GameObject instantiatedButton = Instantiate(_prefabLanguageCnangeButton, popup.Container.Canvas.gameObject.transform);
                    if (instantiatedButton.TryGetComponent(out ChangeLanguageButton changeLanguage))
                    {
                        changeLanguage.Language = localizedLanguage.Language; 
                        Texture2D flagTexture = localizedLanguage.Flag;
                        changeLanguage.ImageLanguage.sprite = Sprite.Create(flagTexture, changeLanguage.ImageLanguage.sprite.rect, changeLanguage.ImageLanguage.sprite.pivot);


                    }
                    if (instantiatedButton.TryGetComponent(out UIButton uiButton))
                    {
                        popup.Data.Buttons.Add(uiButton);
                    }
                }
            }
            
        }
    }
}

