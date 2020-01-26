using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Com.SoftToysFighting.DoozyUI.Prototype
{
    public class ToggleSetting : MonoBehaviour
    {
        public Toggle Toggle;
        public TMP_Text Name;

        private void Awake()
        {
            if (Toggle == null)
            {
                Toggle = GetComponentInChildren<Toggle>();
                Name = Toggle.GetComponentInChildren<TMP_Text>();
            }
        }
    }
}

