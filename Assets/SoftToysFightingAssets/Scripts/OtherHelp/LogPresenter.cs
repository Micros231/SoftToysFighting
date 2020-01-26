using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using TMPro;

namespace Com.SoftToysFighting
{
    [Serializable]
    public class LogPresenter
    {
        [SerializeField] private TMP_Text _textLog;


        public void DebugWriteLine(string message)
        {
        if (_textLog == null)
            Debug.LogError($"TMP_Text is null in {nameof(LogPresenter)}");
        else
            _textLog.text += message + Environment.NewLine;      
            Debug.Log(message);
        }
        public void Clear()
        {
            _textLog.text = string.Empty;
        }
    }
}
