using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InfinityEngine.Localization;

namespace Com.SoftToysFighting.Localization
{
    public class ChangeLanguageButton : MonoBehaviour
    {
        public Language Language;
        public Sprite SpriteLanguage;
        public Image ImageLanguage;

        private void Awake()
        {
            if (SpriteLanguage != null)
            {
                ImageLanguage.sprite = SpriteLanguage;
            }
            name += $"_{Language.ToString()}";
        }
        public void ChangeLanguage()
        {
            ISILocalization.ChangeLanguage(Language);
        }
    }
}

