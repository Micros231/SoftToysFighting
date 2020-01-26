using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.SoftToysFighting.Person;
using Malee;

namespace Com.SoftToysFighting.Settings
{
    [Serializable]
    public class PlayerSettings
    {
        [Reorderable(paginate = true, pageSize = 0,sortable = true)]
        public ReorderablePlayersList Players = new ReorderablePlayersList();
        public Player CurrentPlayer;
    }

    [Serializable]
    public class ReorderablePlayersList : ReorderableArray<Player> { }
}

