/**
 * The MIT License (MIT)
 *
 * Copyright (c) 2012-2017 DragonBones team and other contributors
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of
 * this software and associated documentation files (the "Software"), to deal in
 * the Software without restriction, including without limitation the rights to
 * use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
 * the Software, and to permit persons to whom the Software is furnished to do so,
 * subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
 * FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
 * COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
 * IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
 * CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
﻿using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Reflection;
using UnityEditor.SceneManagement;

namespace DragonBones
{
    [CustomEditor(typeof(UnityArmatureComponent))]
    public class UnityArmatureEditor : Editor
    {
        private long _nowTime = 0;
        private float _frameRate = 1.0f / 24.0f;

        private int _armatureIndex = -1;
        private int _animationIndex = -1;
        private int _sortingModeIndex = -1;
        private int _sortingLayerIndex = -1;

        private List<string> _armatureNames = null;
        private List<string> _animationNames = null;
        private List<string> _sortingLayerNames = null;

        private UnityArmatureComponent _armatureComponent = null;

        private SerializedProperty _playTimesPro;
        private SerializedProperty _timeScalePro;
        private SerializedProperty _flipXPro;
        private SerializedProperty _flipYPro;

        private readonly List<string> _sortingMode = new List<string> { SortingMode.SortByZ.ToString(), SortingMode.SortByOrder.ToString() };

        void ClearUp()
        {
            _armatureIndex = -1;
            _animationIndex = -1;
            _sortingModeIndex = -1;
            _sortingLayerIndex = -1;

            _armatureNames = null;
            _animationNames = null;
            _sortingLayerNames = null;
        }

        void OnDisable()
        {
        }

        void OnEnable()
        {
            _armatureComponent = target as UnityArmatureComponent;
            if (_IsPrefab())
            {
                return;
            }

            // 
            _nowTime = System.DateTime.Now.Ticks;

            _sortingModeIndex = (int)_armatureComponent.sortingMode;
            _sortingLayerNames = _GetSortingLayerNames();
            _sortingLayerIndex = _sortingLayerNames.IndexOf(_armatureComponent.sortingLayerName);

            _playTimesPro = serializedObject.FindProperty("_playTimes");
            _timeScalePro = serializedObject.FindProperty("_timeScale");
            _flipXPro = serializedObject.FindProperty("_flipX");
            _flipYPro = serializedObject.FindProperty("_flipY");

            // Update armature.
            if (!EditorApplication.isPlayingOrWillChangePlaymode &&
                _armatureComponent.armature == null &&
                _armatureComponent.unityData != null &&
                !string.IsNullOrEmpty(_armatureComponent.armatureName))
            {
                // Clear cache
                UnityFactory.factory.Clear(true);

                // Unload
                EditorUtility.UnloadUnusedAssetsImmediate();
                System.GC.Collect();

                // Load data.
                var dragonBonesData = UnityFactory.factory.LoadData(_armatureComponent.unityData);

                // Refresh texture atlas.
                UnityFactory.factory.RefreshAllTextureAtlas(_armatureComponent);

                // Refresh armature.
                UnityEditor.ChangeArmatureData(_armatureComponent, _armatureComponent.armatureName, dragonBonesData.name);

                // Refresh texture.
                _armatureComponent.armature.InvalidUpdate(null, true);

                // Play animation.
                if (!string.IsNullOrEmpty(_armatureComponent.animationName))
                {
                    _armatureComponent.animation.Play(_armatureComponent.animationName, _playTimesPro.intValue);
                }

                _armatureComponent.CollectBones();
            }

            // Update hideFlags.
            if (!EditorApplication.isPlayingOrWillChangePlaymode &&
                _armatureComponent.armature != null &&
                _armatureComponent.armature.parent != null)
            {
                _armatureComponent.gameObject.hideFlags = HideFlags.NotEditable;
            }
            else
            {
                _armatureComponent.gameObject.hideFlags = HideFlags.None;
            }

            _UpdateParameters();
        }
        private void UpdateDragon()
        {
            if (!EditorApplication.isPlayingOrWillChangePlaymode &&
                _armatureComponent.armature == null &&
                _armatureComponent.unityData != null &&
                !string.IsNullOrEmpty(_armatureComponent.armatureName))
            {
                // Clear cache
                UnityFactory.factory.Clear(true);

                // Unload
                EditorUtility.UnloadUnusedAssetsImmediate();
                System.GC.Collect();

                // Load data.
                var dragonBonesData = UnityFactory.factory.LoadData(_armatureComponent.unityData);

                // Refresh texture atlas.
                UnityFactory.factory.RefreshAllTextureAtlas(_armatureComponent);

                // Refresh armature.
                UnityEditor.ChangeArmatureData(_armatureComponent, _armatureComponent.armatureName, dragonBonesData.name);

                // Refresh texture.
                _armatureComponent.armature.InvalidUpdate(null, true);

                // Play animation.
                if (!string.IsNullOrEmpty(_armatureComponent.animationName))
                {
                    _armatureComponent.animation.Play(_armatureComponent.animationName, _playTimesPro.intValue);
                }

                _armatureComponent.CollectBones();
            }
        }
        public override void OnInspectorGUI()
        {
            if (_IsPrefab())
            {
                return;
            }

            serializedObject.Update();

            if (_armatureIndex == -1)
            {
                _UpdateParameters();
            }

            // DragonBones Data
            EditorGUILayout.BeginHorizontal();

            _armatureComponent.unityData = EditorGUILayout.ObjectField("DragonBones Data", _armatureComponent.unityData, typeof(UnityDragonBonesData), false) as UnityDragonBonesData;

            var created = false;
            if (FindObjectsOfType<UnityArmatureComponent>().Length <= 1)
            {
                if (_armatureComponent.unityData != null)
                {
                    if (_armatureComponent.armature == null)
                    {
                        if (GUILayout.Button("Create"))
                        {
                            created = true;
                        }
                    }
                    /*else
                    {
                        if (GUILayout.Button("Reload"))
                        {
                            if (EditorUtility.DisplayDialog("DragonBones Alert", "Are you sure you want to reload data", "Yes", "No"))
                            {
                                created = true;
                            }
                        }
                    }*/
                }
                else
                {
                    //create UnityDragonBonesData by a json data
                    if (GUILayout.Button("JSON"))
                    {
                        PickJsonDataWindow.OpenWindow(_armatureComponent);
                    }
                }
            }
            else
            {
                if (GUILayout.Button("Info"))
                {
                    EditorUtility.DisplayDialog("DragonBones Alert",
                        "У вас на сцене 2 или больше Game Object-ов с UnityArmatureComponent, " +
                        "если вы хотите инициализировать новую модель, то нужно, чтобы на сцене был только " +
                        "один Game Object c UnityArmatureComponent! ",
                        "Понял!");

                }
            }
            

            if (created)
            {
                //clear cache
                UnityFactory.factory.Clear(true);
                ClearUp();
                _armatureComponent.animationName = null;

                if (UnityEditor.ChangeDragonBonesData(_armatureComponent, _armatureComponent.unityData.dragonBonesJSON))
                {
                    _armatureComponent.CollectBones();
                    _UpdateParameters();
                }
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();

            if (_armatureComponent.armature != null)
            {
                var dragonBonesData = _armatureComponent.armature.armatureData.parent;

                // Armature
                if (UnityFactory.factory.GetAllDragonBonesData().ContainsValue(dragonBonesData) && _armatureNames != null)
                {
                    var armatureIndex = EditorGUILayout.Popup("Armature", _armatureIndex, _armatureNames.ToArray());
                    if (_armatureIndex != armatureIndex)
                    {
                        _armatureIndex = armatureIndex;

                        var armatureName = _armatureNames[_armatureIndex];
                        UnityEditor.ChangeArmatureData(_armatureComponent, armatureName, dragonBonesData.name);
                        _UpdateParameters();
                        if (_armatureComponent.bonesRoot != null && _armatureComponent.unityBones != null)
                        {
                            _armatureComponent.ShowBones();
                        }

                        _armatureComponent.gameObject.name = armatureName;

                        MarkSceneDirty();
                    }
                }

                // Animation
                if (_animationNames != null && _animationNames.Count > 0)
                {
                    EditorGUILayout.BeginHorizontal();
                    List<string> anims = new List<string>(_animationNames);
                    anims.Insert(0, "<None>");
                    var animationIndex = EditorGUILayout.Popup("Animation", _animationIndex + 1, anims.ToArray()) - 1;
                    if (animationIndex != _animationIndex)
                    {
                        _animationIndex = animationIndex;
                        if (animationIndex >= 0)
                        {
                            _armatureComponent.animationName = _animationNames[animationIndex];
                            var animationData = _armatureComponent.animation.animations[_armatureComponent.animationName];
                            _armatureComponent.animation.Play(_armatureComponent.animationName, _playTimesPro.intValue);
                            _UpdateParameters();
                        }
                        else
                        {
                            _armatureComponent.animationName = null;
                            _playTimesPro.intValue = 0;
                            _armatureComponent.animation.Stop();
                        }

                        MarkSceneDirty();
                    }

                    if (_animationIndex >= 0)
                    {
                        if (_armatureComponent.animation.isPlaying)
                        {
                            if (GUILayout.Button("Stop"))
                            {
                                _armatureComponent.animation.Stop();
                            }
                        }
                        else
                        {
                            if (GUILayout.Button("Play"))
                            {
                                _armatureComponent.animation.Play(null, _playTimesPro.intValue);
                            }
                        }
                    }
                    EditorGUILayout.EndHorizontal();

                    //playTimes
                    EditorGUILayout.BeginHorizontal();
                    var playTimes = _playTimesPro.intValue;
                    EditorGUILayout.PropertyField(_playTimesPro, false);
                    if (playTimes != _playTimesPro.intValue)
                    {
                        if (!string.IsNullOrEmpty(_armatureComponent.animationName))
                        {
                            _armatureComponent.animation.Reset();
                            _armatureComponent.animation.Play(_armatureComponent.animationName, _playTimesPro.intValue);
                        }
                    }
                    EditorGUILayout.EndHorizontal();

                    // TimeScale
                    var timeScale = _timeScalePro.floatValue;
                    EditorGUILayout.PropertyField(_timeScalePro, false);
                    if (timeScale != _timeScalePro.floatValue)
                    {
                        _armatureComponent.animation.timeScale = _timeScalePro.floatValue;
                    }
                }

                //
                EditorGUILayout.Space();

                if (!_armatureComponent.isUGUI)
                {
                    //Sorting Mode
                    _sortingModeIndex = EditorGUILayout.Popup("Sorting Mode", (int)_armatureComponent.sortingMode, _sortingMode.ToArray());
                    if (_sortingModeIndex != (int)_armatureComponent.sortingMode)
                    {
                        Undo.RecordObject(_armatureComponent, "Sorting Mode");
                        _armatureComponent.sortingMode = (SortingMode)_sortingModeIndex;
                        // 里面return了，没有赋值成功
                        if (_armatureComponent.sortingMode != (SortingMode)_sortingModeIndex)
                        {
                            _sortingModeIndex = (int)_armatureComponent.sortingMode;
                        }

                        MarkSceneDirty();
                    }

                    // Sorting Layer
                    _sortingLayerIndex = EditorGUILayout.Popup("Sorting Layer", _sortingLayerIndex, _sortingLayerNames.ToArray());
                    if (_sortingLayerNames[_sortingLayerIndex] != _armatureComponent.sortingLayerName)
                    {
                        Undo.RecordObject(_armatureComponent, "Sorting Layer");
                        _armatureComponent.sortingLayerName = _sortingLayerNames[_sortingLayerIndex];

                        MarkSceneDirty();
                    }

                    // Sorting Order
                    var sortingOrder = EditorGUILayout.IntField("Order in Layer", _armatureComponent.sortingOrder);
                    if (sortingOrder != _armatureComponent.sortingOrder)
                    {
                        Undo.RecordObject(_armatureComponent, "Edit Sorting Order");
                        _armatureComponent.sortingOrder = sortingOrder;

                        MarkSceneDirty();
                    }

                    // ZSpace
                    EditorGUILayout.BeginHorizontal();
                    _armatureComponent.zSpace = EditorGUILayout.Slider("Z Space", _armatureComponent.zSpace, 0.0f, 0.5f);
                    EditorGUILayout.EndHorizontal();
                }

                EditorGUILayout.Space();

                // Flip
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("Flip");
                var flipX = _flipXPro.boolValue;
                var flipY = _flipYPro.boolValue;
                _flipXPro.boolValue = GUILayout.Toggle(_flipXPro.boolValue, "X", GUILayout.Width(30));
                _flipYPro.boolValue = GUILayout.Toggle(_flipYPro.boolValue, "Y", GUILayout.Width(30));
                if (flipX != _flipXPro.boolValue || flipY != _flipYPro.boolValue)
                {
                    _armatureComponent.armature.flipX = _flipXPro.boolValue;
                    _armatureComponent.armature.flipY = _flipYPro.boolValue;

                    MarkSceneDirty();
                }

                EditorGUILayout.EndHorizontal();

                EditorGUILayout.Space();

                //normals
                EditorGUILayout.BeginHorizontal();
                _armatureComponent.addNormal = EditorGUILayout.Toggle("Normals", _armatureComponent.addNormal);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space();
            }

            if (_armatureComponent.armature != null && _armatureComponent.armature.parent == null)
            {
                if (_armatureComponent.unityBones != null && _armatureComponent.bonesRoot != null)
                {
                    _armatureComponent.boneHierarchy = EditorGUILayout.Toggle("Bone Hierarchy", _armatureComponent.boneHierarchy);
                }

                EditorGUILayout.BeginHorizontal();
                if (!Application.isPlaying)
                {
                    if (_armatureComponent.unityBones != null && _armatureComponent.bonesRoot != null)
                    {
                        if (GUILayout.Button("Remove Bones", GUILayout.Height(20)))
                        {
                            if (EditorUtility.DisplayDialog("DragonBones Alert", "Are you sure you want to remove bones", "Yes", "No"))
                            {
                                _armatureComponent.RemoveBones();
                            }
                        }
                    }
                    else
                    {
                        if (GUILayout.Button("Show Bones", GUILayout.Height(20)))
                        {
                            _armatureComponent.ShowBones();
                        }
                    }
                }
                if (!Application.isPlaying && !_armatureComponent.isUGUI)
                {
                    UnityCombineMesh ucm = _armatureComponent.gameObject.GetComponent<UnityCombineMesh>();
                    if (!ucm)
                    {
                        if (GUILayout.Button("Add Mesh Combine", GUILayout.Height(20)))
                        {
                            ucm = _armatureComponent.gameObject.AddComponent<UnityCombineMesh>();
                        }
                    }
                }
                EditorGUILayout.EndHorizontal();
            }

            serializedObject.ApplyModifiedProperties();

            if (!EditorApplication.isPlayingOrWillChangePlaymode && Selection.activeObject == _armatureComponent.gameObject)
            {
                EditorUtility.SetDirty(_armatureComponent);
                HandleUtility.Repaint();
            }
        }

        void OnSceneGUI()
        {
            if (!EditorApplication.isPlayingOrWillChangePlaymode && _armatureComponent.armature != null)
            {
                var dt = (System.DateTime.Now.Ticks - _nowTime) * 0.0000001f;
                if (dt >= _frameRate)
                {
                    _armatureComponent.armature.AdvanceTime(dt);

                    foreach (var slot in _armatureComponent.armature.GetSlots())
                    {
                        if (slot.childArmature != null)
                        {
                            slot.childArmature.AdvanceTime(dt);
                        }
                    }

                    //
                    _nowTime = System.DateTime.Now.Ticks;
                }
            }
        }

        private void _UpdateParameters()
        {
            if (_armatureComponent.armature != null)
            {
                _frameRate = 1.0f / (float)_armatureComponent.armature.armatureData.frameRate;

                if (_armatureComponent.armature.armatureData.parent != null)
                {
                    _armatureNames = _armatureComponent.armature.armatureData.parent.armatureNames;
                    _animationNames = _armatureComponent.armature.armatureData.animationNames;
                    _armatureIndex = _armatureNames.IndexOf(_armatureComponent.armature.name);
                    //
                    if (!string.IsNullOrEmpty(_armatureComponent.animationName))
                    {
                        _animationIndex = _animationNames.IndexOf(_armatureComponent.animationName);
                    }
                }
                else
                {
                    _armatureNames = null;
                    _animationNames = null;
                    _armatureIndex = -1;
                    _animationIndex = -1;
                }
            }
            else
            {
                _armatureNames = null;
                _animationNames = null;
                _armatureIndex = -1;
                _animationIndex = -1;
            }
        }

        private bool _IsPrefab()
        {
            return PrefabUtility.GetPrefabParent(_armatureComponent.gameObject) == null
                && PrefabUtility.GetPrefabObject(_armatureComponent.gameObject) != null;
        }

        private List<string> _GetSortingLayerNames()
        {
            var internalEditorUtilityType = typeof(InternalEditorUtility);
            var sortingLayersProperty = internalEditorUtilityType.GetProperty("sortingLayerNames", BindingFlags.Static | BindingFlags.NonPublic);

            return new List<string>(sortingLayersProperty.GetValue(null, new object[0]) as string[]);
        }

        private void MarkSceneDirty()
        {
            EditorUtility.SetDirty(_armatureComponent);
            //
            if (!Application.isPlaying && !_IsPrefab())
            {
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            }
        }
    }
}