using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine.SceneManagement;

namespace Com.SoftToysFighting
{
    public class SceneController : MonoBehaviour
    {
        private SceneLoader _sceneLoader;

        private void Awake()
        {
            _sceneLoader = GameObject.FindWithTag("SceneLoader").GetComponent<SceneLoader>();
        }

        public void LoadMainMenu()
        {
            _sceneLoader.LoadSceneAsyncSingle(0);
        }
    }
}

