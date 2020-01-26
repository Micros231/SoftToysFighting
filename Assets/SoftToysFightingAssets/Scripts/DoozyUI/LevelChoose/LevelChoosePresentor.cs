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
    public class LevelChoosePresentor : MonoBehaviour
    {
        #region Public Properties
        public bool IsSelected => _isSelected;
        public bool IsAvailable => _isAvailable;
        public string LevelName
        {
            get => _levelName;
            set
            {
                _textLevelName.text = value;
                _levelName = value;
            }
        }
        public UIButton ButtonChooseLevel => _buttonChooseLevel;
        public UIButton ButtonPlay => _buttonPlay;
        public Image ImageBackground => _imageBackground;
        #endregion

        #region Private SerializeFields
        [SerializeField]
        private bool _isSelected;
        [SerializeField]
        private bool _isAvailable;
        [SerializeField]
        private string _levelName;
        [SerializeField]
        private Image _imageStandart;
        [SerializeField]
        private Image _imageSelected;
        [SerializeField]
        private Image _imageBackground;
        [SerializeField]
        private Image _imageForeground;
        [SerializeField]
        private TMP_Text _textLevelName;
        [SerializeField]
        private UIButton _buttonChooseLevel;
        [SerializeField]
        private UIButton _buttonPlay;
        [Header("Colors Foreground")] 
        [SerializeField]
        private Color _colorAvailable;
        [SerializeField]
        private Color _colorNotAvailable;
        #endregion

        #region MonoBehaviour Callbacks
        private void OnDestroy()
        {
            _buttonChooseLevel.OnClick.OnTrigger.Event.RemoveAllListeners();
            _buttonPlay.OnClick.OnTrigger.Event.RemoveAllListeners();
        }
        #endregion

        #region Public Methods
        public void SelectLevel()
        {
            _isSelected = true;
            _imageStandart.enabled = false;
            _imageSelected.enabled = true;
            if (_isAvailable)
            {
                _buttonPlay.gameObject.SetActive(true);
            }
            
        }
        public void DeselectLevel()
        {
            _isSelected = false;
            _imageStandart.enabled = true;
            _imageSelected.enabled = false;
            _buttonPlay.gameObject.SetActive(false);
            
        }
        public void AvailableLevel()
        {
            _isAvailable = true;
            _imageForeground.color = _colorAvailable;
            if (_isSelected)
            {
                _buttonPlay.gameObject.SetActive(true);
            }
            else
            {
                _buttonPlay.gameObject.SetActive(false);
            }
            
        }
        public void NotAvailableLevel()
        {
            _isAvailable = false;
            _imageForeground.color = _colorNotAvailable;
            _buttonPlay.gameObject.SetActive(false);
        }
        #endregion

    }
}

