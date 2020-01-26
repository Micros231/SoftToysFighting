using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.SoftToysFighting.Person;
using Com.SoftToysFighting.Person.Enemies.Spawner;
using Malee;

namespace Com.SoftToysFighting.Settings
{
    [Serializable]

    public class EnemySettings
    {
        public SpawnStateBase SpawnState;
        public List<Enemy> Enemies = new List<Enemy>();
        public int AmountEnemiesInLevel;
        public int MaxEnemiesAliveInLevel;
    }
}

