using UnityEngine;
using System.Collections;

namespace Com.SoftToysFighting
{
    public class CameraFollowToPlayerSinglePlayer : CameraFollowToPerson
    {
        [SerializeField] private Transform  _spawnersTransform;

        protected override void InitCamera()
        {
            _spawnersTransform.localScale = new Vector2(_horizontalExtent + 1, 1);
        }
    }
}

