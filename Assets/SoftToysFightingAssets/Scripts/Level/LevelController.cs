using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace  Com.SoftToysFighting.LevelSystem
{ 
    public class LevelController : MonoBehaviour
    {
        public EndZone[] EndZones 
        { 
            get => _endZones; 
            private set => _endZones = value; }

        [SerializeField]
        private Color _colorEndZones = Color.blue;
        [SerializeField]
        private EndZone[] _endZones = new EndZone[4];

        private void Awake()
        {
            EndZones = GetComponentsInChildren<EndZone>();
        }

        private void OnDrawGizmosSelected()
        {
            EndZones = GetComponentsInChildren<EndZone>();
            Gizmos.color = _colorEndZones;
            var UpEndZone = GetEndZone(EndZone.DirectionEndZone.Up).transform.position;
            var DownEndZone = GetEndZone(EndZone.DirectionEndZone.Down).transform.position;
            var RightEndZone = GetEndZone(EndZone.DirectionEndZone.Right).transform.position;
            var LeftEndZone = GetEndZone(EndZone.DirectionEndZone.Left).transform.position;
            Gizmos.DrawLine(new Vector2(RightEndZone.x, UpEndZone.y), new Vector2(LeftEndZone.x, UpEndZone.y));
            Gizmos.DrawLine(new Vector2(RightEndZone.x, DownEndZone.y), new Vector2(LeftEndZone.x, DownEndZone.y));
            Gizmos.DrawLine(new Vector2(RightEndZone.x, UpEndZone.y), new Vector2(RightEndZone.x, DownEndZone.y));
            Gizmos.DrawLine(new Vector2(LeftEndZone.x, UpEndZone.y), new Vector2(LeftEndZone.x, DownEndZone.y));
        }

        public EndZone GetEndZone(EndZone.DirectionEndZone directionEndZone)
        {
            EndZones = GetComponentsInChildren<EndZone>();
            if (EndZones == null)
            {
                throw new ArgumentNullException("EndZones", "EndZones is null");
            }
            EndZone endZone = null;
            foreach (var zone in EndZones)
            {
                if (zone.DirectionZone == directionEndZone)
                {
                    endZone = zone;
                }
            }
            if (endZone == null)
            {
                throw new ArgumentNullException("endZone", $"EndZone({directionEndZone.ToString()}) is not found in EndZones");
            }
            return endZone;
        }

    }

}

