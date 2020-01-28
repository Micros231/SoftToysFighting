using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Com.SoftToysFighting.DoozyUI
{
    public abstract class PresentorEnteties : MonoBehaviour
    {
        public bool IsSelected => _isSelected;
        public bool IsAvailable => _isAvailable;
        public string EntityName
        {
            get => _entityName;
            set
            {
                _entityName = value;
                _textEntityName.text = _entityName;
            }
        }
        public Image ImageForeground => _imageForeground;
        public Image ImageBackground => _imageBackground;

        [SerializeField]
        private bool _isSelected;
        [SerializeField]
        private bool _isAvailable;
        [SerializeField]
        private string _entityName;
        [SerializeField]
        private Image _imageForeground;
        [SerializeField]
        private Image _imageBackground;
        [SerializeField]
        private Image _imageStandart;
        [SerializeField]
        private Image _imageSelected;
        [SerializeField]
        private TMP_Text _textEntityName;

        [Header("Colors Foreground")]
        [SerializeField]
        private Color _colorAvailable;
        [SerializeField]
        private Color _colorNotAvailable;

        public void Select()
        {
            _isSelected = true;
            _imageStandart.enabled = false;
            _imageSelected.enabled = true;
            SelectEntity();
        }
        public void Deselect()
        {
            _isSelected = false;
            _imageStandart.enabled = true;
            _imageSelected.enabled = false;
            DeselectEntity();
        }
        public void Available()
        {
            _isAvailable = true;
            _imageForeground.color = _colorAvailable;
            if (_isSelected)
            {
                _imageStandart.enabled = false;
                _imageSelected.enabled = true;
            }
            else
            {
                _imageStandart.enabled = true;
                _imageSelected.enabled = false;
            }
            AvailableEntity();
        }
        public void Unavailable()
        {
            _isAvailable = false;
            _imageForeground.color = _colorNotAvailable;
            if (_isSelected)
            {
                _imageStandart.enabled = false;
                _imageSelected.enabled = true;
            }
            else
            {
                _imageStandart.enabled = true;
                _imageSelected.enabled = false;
            }
            UnavailableEntity();
        }

        protected abstract void SelectEntity();
        protected abstract void DeselectEntity();
        protected abstract void AvailableEntity();
        protected abstract void UnavailableEntity();
    }
}

