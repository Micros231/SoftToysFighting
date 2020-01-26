using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.SoftToysFighting.LevelSystem
{
    
    public class EndZone : MonoBehaviour
    {
        public enum DirectionEndZone
        {
            Up = 0,
            Down = 1,
            Left = 2,
            Right = 3
        }
        public DirectionEndZone DirectionZone => _directionEndZone;

        [SerializeField]
        private DirectionEndZone _directionEndZone;
    }
}

