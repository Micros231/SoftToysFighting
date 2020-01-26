using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Malee;

namespace Com.SoftToysFighting.Settings
{
    [Serializable]
    public class LevelSettings
    {
        public int SceneBuildIndexToLoad => _buildIndex;
        public List<Level> Levels = new List<Level>();
        public Level CurrentLevel;


        [SerializeField]
        private int _buildIndex;
    }
}

