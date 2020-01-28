using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

namespace Com.SoftToysFighting.DoozyUI
{
    public class ToyPresentor : PresentorEnteties
    {
        #region Public Properties
        public string ToyDescription
        {
            get => _toyDescription;
            set
            {
                _toyDescription = value;
                _textToyDescription.text = _toyDescription;
            }
        }
        public float ToyPrice
        {
            get => _toyPrice;
            set => _toyPrice = value;
        }
        public Image ImageToy => _imageToy;
        #endregion

        #region Private SerializeFields
        [SerializeField]
        private string _toyDescription;
        [SerializeField]
        private float _toyPrice;
        [SerializeField]
        private Image _imageToy;
        [SerializeField]
        private TMP_Text _textToyDescription;
        #endregion

        protected override void SelectEntity()
        {
            ToyDescription = "Selected";
        }

        protected override void DeselectEntity()
        {
            ToyDescription = IsAvailable ? "Available" : _toyPrice.ToString();
        }

        protected override void AvailableEntity()
        {
            ToyDescription = IsSelected ? "Selected" : "Available";
        }

        protected override void UnavailableEntity()
        {
            ToyDescription = _toyPrice.ToString();
        }
    }
}


