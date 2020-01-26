using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.SoftToysFighting.Settings
{
    [Serializable]
    public class Level : Entity
    {
        public Sprite LevelSprite;
        public EnemySettings EnemySettingsInLevel;
        public override List<ParameterFloat> GetParameters()
        {
            return null;
        }
    }
}
