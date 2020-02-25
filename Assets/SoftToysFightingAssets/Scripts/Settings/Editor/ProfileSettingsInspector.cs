using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace Com.SoftToysFighting.Settings.Editors
{
    [CustomEditor(typeof(ProfileSettings))]
    public class ProfileSettingsInspector : Editor
    {
        private ProfileSettings ProfileSettings => target as ProfileSettings;
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            base.OnInspectorGUI();
            if (GUILayout.Button("NullableCurrentPlayer"))
            {
                NullableCurrentPlayer();
            }
            serializedObject.ApplyModifiedProperties();
            EditorFix.SetObjectDirty(ProfileSettings);
        }

        private void NullableCurrentPlayer()
        {
            if (ProfileSettings.PlayerSettings.CurrentPlayer == null)
            {
                return;
            }
            ProfileSettings.PlayerSettings.CurrentPlayer = new Player();
        }

        private void CloneAndUpdatePlayers()
        {
            ReorderablePlayersList clonnedPlayers = new ReorderablePlayersList();
            foreach (var player in ProfileSettings.PlayerSettings.Players)
            {
                clonnedPlayers.Add(ClonePlayer(player));
            }
            ProfileSettings.PlayerSettings.Players = clonnedPlayers;
        }
        private Player ClonePlayer(Player target)
        {
            if (target == null)
            {
                return null;
            }
            return new Player
            {
                Name = target.Name,
                Prefab = target.Prefab,
                AnimationTimeScale = target.AnimationTimeScale,
                DamageHand = target.DamageHand,
                DamageLeg = target.DamageLeg,
                DamageSuper = target.DamageSuper,
                IsAvailable = target.IsAvailable,
                MaxEnergy = target.MaxEnergy,
                MaxHealth = target.MaxHealth,
                MoveSpeed = target.MoveSpeed
            };
        }
    }
}

