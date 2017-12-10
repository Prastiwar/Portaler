using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(CollisionManager))]
[CanEditMultipleObjects]
public class CollisionManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var colManager = target as CollisionManager;
        
        switch (colManager.colItemType)
        {
            case CollisionManager.ColliderItemType.Portal:
                colManager.isItFirstPortal = EditorGUILayout.Toggle("Is it first portal?", colManager.isItFirstPortal);
                break;

            case CollisionManager.ColliderItemType.StealItem:
                EditorGUILayout.ObjectField("Scriptable Steal Item", colManager.stealItem, typeof(ScriptableStealItem), true);
                break;

            default:
                break;
        }
    }
    
}
