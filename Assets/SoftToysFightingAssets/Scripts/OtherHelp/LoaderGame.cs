using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine;
using Doozy.Engine.UI;

public class LoaderGame : MonoBehaviour
{
    private static bool IsFirstStart = true;
    [SerializeField]
    private UIView _startGameView;
    private void Start()
    {
        if (_startGameView == null)
        {
            throw new NullReferenceException("StartGameView is null");
        }
        _startGameView.ShowBehavior.OnFinished.Event.AddListener(InitializeGame);
        
    }
    private void OnDestroy()
    {
        _startGameView.ShowBehavior.OnFinished.Event.RemoveListener(InitializeGame);
    }
    private void InitializeGame()
    {
        if (IsFirstStart)
        {
            GameEventMessage.SendEvent("GoToStartScreenEvent");
            IsFirstStart = false;
        }
        else
        {
            GameEventMessage.SendEvent("GoToMainMenuEvent");
        }
    }
    

}
