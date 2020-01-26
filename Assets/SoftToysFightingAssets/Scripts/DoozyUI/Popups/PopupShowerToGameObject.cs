using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine.UI;

namespace Com.SoftToysFighting.DoozyUI
{
    public class PopupShowerToGameObject : PopupShowerBase
    {
        [SerializeField]
        private UIPopup popup;
        public override void ShowPopup()
        {
            if (popup != null)
            {
                popup.Show();
            }
            else
            {
                Debug.LogError($"Popup is null");
            }
        }
    }
}

