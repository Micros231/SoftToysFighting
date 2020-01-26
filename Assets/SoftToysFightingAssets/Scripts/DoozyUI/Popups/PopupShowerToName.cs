using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine.UI;
namespace Com.SoftToysFighting.DoozyUI
{
    public class PopupShowerToName : PopupShowerBase
    {
        [SerializeField]
        private string _name;
        public override void ShowPopup()
        {
            UIPopup popup = UIPopup.GetPopup(_name);
            if (popup != null)
            {
                popup.Show();
            }
            else
            {
                Debug.LogError("Popup is null");
            }

            
        }
    }
}

