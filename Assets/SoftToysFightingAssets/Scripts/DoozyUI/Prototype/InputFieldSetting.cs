using UnityEngine;
using TMPro;

namespace Com.SoftToysFighting.DoozyUI.Prototype
{
    public class InputFieldSetting : MonoBehaviour
    {
        public TMP_Text Name;
        public TMP_InputField Input;

        private void Awake()
        {
            if (Name == null)
                Name = GetComponentInChildren<TMP_Text>();
            if (Input == null)
                Input = GetComponentInChildren<TMP_InputField>();
        }
    }
}

