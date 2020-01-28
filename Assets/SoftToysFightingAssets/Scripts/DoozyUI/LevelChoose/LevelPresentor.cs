using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Doozy.Engine.UI;
using TMPro;
using UnityEngine.Events;
using System;

namespace Com.SoftToysFighting.DoozyUI
{
    public class LevelPresentor : PresentorEnteties
    {
        #region Public Properties
        public UIButton ButtonChooseLevel => _buttonChooseLevel;
        public UIButton ButtonPlay => _buttonPlay;
        #endregion

        #region Private SerializeFields
        [SerializeField]
        private UIButton _buttonChooseLevel;
        [SerializeField]
        private UIButton _buttonPlay;
        #endregion

        #region MonoBehaviour Callbacks
        private void OnDestroy()
        {
            _buttonChooseLevel.OnClick.OnTrigger.Event.RemoveAllListeners();
            _buttonPlay.OnClick.OnTrigger.Event.RemoveAllListeners();
        }
        #endregion

        #region Public Methods
        protected override void SelectEntity()
        {
            if (IsAvailable)
            {
                _buttonPlay.gameObject.SetActive(true);
            }
        }

        protected override void DeselectEntity()
        {
            _buttonPlay.gameObject.SetActive(false);
        }

        protected override void AvailableEntity()
        {
            if (IsSelected)
            {
                _buttonPlay.gameObject.SetActive(true);
            }
            else
            {
                _buttonPlay.gameObject.SetActive(false);
            }
        }

        protected override void UnavailableEntity()
        {
            _buttonPlay.gameObject.SetActive(false);
        }
        #endregion

    }
}

