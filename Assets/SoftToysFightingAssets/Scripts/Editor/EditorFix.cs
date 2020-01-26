using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Com.SoftToysFighting 
{
    public static class EditorFix
    {
        public static void SetObjectDirty(Object obj)
        {
            EditorUtility.SetDirty(obj);
        }

        public static void SetObjectDirty(GameObject gameObject)
        {
            EditorUtility.SetDirty(gameObject);
            EditorSceneManager.MarkSceneDirty(gameObject.scene);
        }

        public static void SetObjectDirty(Component component)
        {
            EditorUtility.SetDirty(component);
            EditorSceneManager.MarkSceneDirty(component.gameObject.scene);
        }
    }
}


