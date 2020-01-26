using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Com.SoftToysFighting.Joysticks;

namespace Com.SoftToysFighting.Joysticks.Editors
{
    [CustomEditor(typeof(Joystick)), CanEditMultipleObjects]
    public class JoystickCustomEditor : Editor
    {
        private Joystick _joystick;
        private SerializedProperty _joystickTemplateBridgesProperty;

        private void OnEnable()
        {
            _joystick = (Joystick)target;
            _joystickTemplateBridgesProperty = serializedObject.FindProperty("JoystickTemplateBridges");
            //InitSettings();
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            /*serializedObject.Update();
            EditorGUILayout.Vector2Field("Direction", _joystick.Direction);
            GUILayout.Button("CreateSettings");
            EditorUtility.SetDirty(_joystick);
            serializedObject.ApplyModifiedProperties();*/
        }

        private void InitSettings()
        {
            if (Resources.Load<JoystickSettings>("SoftToysFightingAssets/Scripts/Joystick/") == null)
            {
                JoystickSettings joystickSettings = CreateInstance<JoystickSettings>();
                //AssetDatabase.CreateAsset(joystickSettings, "Assets/SoftToysFightingAssets/Scripts/Joystick/Resources/JoystickSettings.asset");
            }
        }
    }
}

